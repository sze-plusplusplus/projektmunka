import { Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import {
  LocalParticipant,
  LocalTrack,
  Participant,
  ParticipantEvent,
  Track,
  TrackPublication
} from 'livekit-client';

@Component({
  selector: 'livekit-participant',
  templateUrl: './participant.component.html',
  styleUrls: ['./participant.component.scss']
})
export class ParticipantComponent implements OnInit, OnDestroy {
  @Input() participant!: Participant;

  isLocal = false;

  track?: TrackPublication;

  ngOnInit(): void {
    this.participant
      .on(ParticipantEvent.TrackPublished, this.onPublicationsChanged)
      .on(ParticipantEvent.TrackUnpublished, this.onPublicationsChanged)
      .on(ParticipantEvent.TrackSubscribed, this.onPublicationsChanged)
      .on(ParticipantEvent.TrackUnsubscribed, this.onPublicationsChanged)
      .on(ParticipantEvent.TrackMuted, this.onPublicationsChanged)
      .on(ParticipantEvent.TrackUnmuted, this.onPublicationsChanged)
      .on(ParticipantEvent.LocalTrackPublished, this.onPublicationsChanged)
      .on('localtrackchanged', this.onPublicationsChanged);

    // set initial state
    this.onPublicationsChanged();
  }

  ngOnDestroy() {
    if (this.track) {
      this.track.track?.detach();
    }

    // cleanup
    this.participant
      .off(ParticipantEvent.TrackPublished, this.onPublicationsChanged)
      .off(ParticipantEvent.TrackUnpublished, this.onPublicationsChanged)
      .off(ParticipantEvent.TrackSubscribed, this.onPublicationsChanged)
      .off(ParticipantEvent.TrackUnsubscribed, this.onPublicationsChanged)
      .off(ParticipantEvent.TrackMuted, this.onPublicationsChanged)
      .off(ParticipantEvent.TrackUnmuted, this.onPublicationsChanged)
      .off(ParticipantEvent.LocalTrackPublished, this.onPublicationsChanged)
      .off('localtrackchanged', this.onPublicationsChanged);
  }

  onPublicationsChanged = () => {
    this.isLocal = this.participant instanceof LocalParticipant;

    const video = this.participant.getTrack(Track.Source.Camera);

    if (video && (video.isSubscribed || this.isLocal)) {
      const track = video?.track?.attach();

      if (track) {
        if (this.track?.track && this.track.trackSid !== video.trackSid) {
          this.detach();
        } else if (!this.track?.track) {
          this.track = video;
          document
            .getElementById('video-' + this.participant.sid)
            ?.appendChild(track);
        }
      }
    } else if (this.track?.track) {
      this.detach();
    }
  };

  detach = () => {
    if (this.track?.track) {
      document
        .getElementById('video-' + this.participant.sid)
        ?.removeChild(this.track.track.attachedElements[0]);
      this.track?.track?.detach();
      this.track = undefined;
    }
  };
}
