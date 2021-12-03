import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { LocalParticipant } from 'livekit-client';

@Component({
  selector: 'livekit-controls',
  templateUrl: './controls.component.html',
  styleUrls: ['./controls.component.scss']
})
export class ControlsComponent implements OnInit {
  @Input()
  localParticipant!: LocalParticipant;

  @Output()
  leave = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}

  mute() {
    this.localParticipant.setMicrophoneEnabled(
      !this.localParticipant.isMicrophoneEnabled
    );
  }

  toggleVideo() {
    this.localParticipant.setCameraEnabled(
      !this.localParticipant.isCameraEnabled
    );
  }

  toggleScreenShare() {
    this.localParticipant.setScreenShareEnabled(
      !this.localParticipant.isScreenShareEnabled
    );
  }
}
