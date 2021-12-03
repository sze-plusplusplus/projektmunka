import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output
} from '@angular/core';
import {
  AudioTrack,
  connect,
  LocalParticipant,
  LogLevel,
  Participant,
  RemoteTrack,
  Room,
  RoomEvent,
  Track,
  VideoTrack
} from 'livekit-client';

@Component({
  selector: 'app-livekit',
  templateUrl: './livekit.component.html',
  styleUrls: ['./livekit.component.scss']
})
export class LivekitComponent implements OnInit, OnDestroy {
  @Input() token!: string;

  @Output()
  leave = new EventEmitter();

  isConnecting: boolean;
  room?: Room;
  participants: Participant[];

  audioTracks: AudioTrack[];
  audioElement: HTMLDivElement;

  screenTrack?: VideoTrack;

  userNumberLimit = 10;

  error?: Error;

  constructor() {
    this.isConnecting = true;
    this.participants = [];

    this.audioTracks = [];
    this.audioElement = document.createElement('div');
    document.body.append(this.audioElement);
  }

  async ngOnInit() {
    this.isConnecting = true;
    this.error = undefined;

    try {
      this.room = await connect('http://localhost:7880', this.token, {
        autoManageVideo: true,
        logLevel: LogLevel.info,
        publishDefaults: { simulcast: true }
      });
    } catch (e) {
      if (e instanceof Error) {
        this.error = e;
      } else {
        this.error = new Error('an error has occured');
      }
    }

    this.onConnected();
  }

  ngOnDestroy(): void {
    if (!this.room) {
      return;
    }
    this.room
      .off(RoomEvent.TrackSubscribed, this.onSubscribedTrackChanged)
      .off(RoomEvent.TrackUnsubscribed, this.onSubscribedTrackChanged)
      .off(RoomEvent.LocalTrackUnpublished, this.onSubscribedTrackChanged)
      .off(RoomEvent.ParticipantConnected, this.onParticipantsChanged)
      .off(RoomEvent.ParticipantDisconnected, this.onParticipantsChanged)
      .off(RoomEvent.ActiveSpeakersChanged, this.onParticipantsChanged)
      .off(RoomEvent.Disconnected, this.handleDisconnect)
      .localParticipant.off('localtrackchanged', this.onSubscribedTrackChanged);

    this.room.disconnect(true);
    this.room.engine.client.close();
    this.audioElement.remove();
  }

  leaveRoom() {
    this.ngOnDestroy();
    this.leave.emit();
  }

  onConnected = () => {
    this.isConnecting = false;

    if (!this.room) {
      return;
    }
    console.log('connected to room', this.room.name);
    console.log('participants in room:', this.room.participants.size);

    this.room
      .on(RoomEvent.TrackSubscribed, this.onSubscribedTrackChanged)
      .on(RoomEvent.TrackUnsubscribed, this.onSubscribedTrackChanged)
      .on(RoomEvent.LocalTrackUnpublished, this.onSubscribedTrackChanged)
      .on(RoomEvent.ParticipantConnected, this.onParticipantsChanged)
      .on(RoomEvent.ParticipantDisconnected, this.onParticipantsChanged)
      .on(RoomEvent.ActiveSpeakersChanged, this.onParticipantsChanged)
      .on(RoomEvent.Disconnected, this.handleDisconnect)
      .localParticipant.on('localtrackchanged', this.onSubscribedTrackChanged);

    this.onSubscribedTrackChanged();
  };

  onSubscribedTrackChanged = (track?: RemoteTrack) => {
    this.onParticipantsChanged();

    if (!this.room || (track && track.kind !== Track.Kind.Audio)) {
      if (
        track?.kind === Track.Kind.Video &&
        track.source === Track.Source.ScreenShare
      ) {
        this.updateScreenShare();
      }
      return;
    }

    const tracks: AudioTrack[] = [];
    this.room.participants.forEach((p) => {
      p.audioTracks.forEach((pub) => {
        if (pub.track && pub.kind === Track.Kind.Audio) {
          tracks.push(pub.track);
        }
      });
    });
    this.removeOldAudioTracks(tracks);
    this.audioTracks = tracks;
    this.addNewAudioTracks();
  };

  onParticipantsChanged = () => {
    if (!this.room) {
      return;
    }

    const remotes = Array.from(this.room.participants.values());
    const participants: Participant[] = [this.room.localParticipant];
    participants.push(...remotes);
    this.sortParticipants(participants, this.room.localParticipant);
    this.participants = participants;
  };

  handleDisconnect = () => {
    console.log('disconnected from room');
  };

  removeOldAudioTracks = (newList: AudioTrack[]) => {
    this.audioTracks.forEach((a) => {
      if (!newList.some((n) => n.sid === a.sid && n.name === a.name)) {
        a.detach();
      }
    });
  };

  addNewAudioTracks = () => {
    this.audioTracks.forEach((a) => {
      if (a.attachedElements.length === 0) {
        this.audioElement.appendChild(a.attach());
      }
    });
  };

  updateScreenShare = () => {
    let screenTrack: VideoTrack | undefined;
    this.participants.forEach((p) => {
      if (screenTrack) {
        return;
      }
      const track = p.getTrack(Track.Source.ScreenShare);
      if (track?.isSubscribed && track.videoTrack) {
        screenTrack = track.videoTrack;
      }
    });
    this.screenTrack = screenTrack;
  };

  /**
   * Default sort for participants, it'll order participants by:
   * 1. dominant speaker (speaker with the loudest audio level)
   * 2. local participant
   * 3. other speakers that are recently active
   * 4. participants with video on
   * 5. by joinedAt
   * 6. local participant is the last
   */
  sortParticipants(
    participants: Participant[],
    localParticipant?: LocalParticipant
  ) {
    participants.sort((a, b) => {
      // loudest speaker first
      if (a.isSpeaking && b.isSpeaking) {
        return b.audioLevel - a.audioLevel;
      }

      // speaker goes first
      if (a.isSpeaking !== b.isSpeaking) {
        if (a.isSpeaking) {
          return -1;
        } else {
          return 1;
        }
      }

      // last active speaker first
      if (a.lastSpokeAt !== b.lastSpokeAt) {
        const aLast = a.lastSpokeAt?.getTime() ?? 0;
        const bLast = b.lastSpokeAt?.getTime() ?? 0;
        return bLast - aLast;
      }

      // video on
      const aVideo = a.videoTracks.size > 0;
      const bVideo = b.videoTracks.size > 0;
      if (aVideo !== bVideo) {
        if (aVideo) {
          return -1;
        } else {
          return 1;
        }
      }

      // joinedAt
      return (a.joinedAt?.getTime() ?? 0) - (b.joinedAt?.getTime() ?? 0);
    });

    // localParticipant is the last, will be rendered differently
    if (localParticipant) {
      const localIdx = participants.indexOf(localParticipant);
      if (localIdx >= 0) {
        participants.splice(localIdx, 1);
        participants.push(localParticipant);
      }
    }
  }
}
