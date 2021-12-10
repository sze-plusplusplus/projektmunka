import { Component, Inject, OnDestroy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Room } from 'livekit-client';

@Component({
  selector: 'livekit-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
  host: {
    class: 'app-dialog'
  }
})
export class SettingsComponent implements OnDestroy {
  audioDevices?: MediaDeviceInfo[];
  videoDevices?: MediaDeviceInfo[];
  audioOutput?: MediaDeviceInfo[];

  constructor(
    public dialogRef: MatDialogRef<boolean>,
    @Inject(MAT_DIALOG_DATA) public data: Room
  ) {
    this.listAudioDevices();
    navigator.mediaDevices.addEventListener(
      'devicechange',
      this.listAudioDevices
    );
  }

  ngOnDestroy() {
    navigator.mediaDevices.removeEventListener(
      'devicechange',
      this.listAudioDevices
    );
  }

  async listAudioDevices() {
    this.audioDevices = await Room.getLocalDevices('audioinput');

    this.videoDevices = await Room.getLocalDevices('videoinput');

    this.audioOutput = await Room.getLocalDevices('audiooutput');
  }

  switchAudio(deviceId: string) {
    this.data.switchActiveDevice('audioinput', deviceId);
  }

  switchVideo(deviceId: string) {
    this.data.switchActiveDevice('videoinput', deviceId);
  }

  switchOutput(deviceId: string) {
    this.data.switchActiveDevice('audiooutput', deviceId);
  }
}
