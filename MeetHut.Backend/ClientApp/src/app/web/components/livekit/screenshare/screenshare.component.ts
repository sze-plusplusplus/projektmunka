import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { VideoTrack } from 'livekit-client';

@Component({
  selector: 'livekit-screenshare',
  templateUrl: './screenshare.component.html',
  styleUrls: ['./screenshare.component.scss']
})
export class ScreenshareComponent implements OnInit, OnChanges {
  @Input() track?: VideoTrack;

  oldTrack?: VideoTrack;

  constructor() {}

  ngOnInit(): void {}

  ngOnChanges() {
    if (
      this.oldTrack &&
      (!this.track || this.track.sid !== this.oldTrack.sid)
    ) {
      this.oldTrack.detach();
      this.oldTrack = undefined;
      document.querySelector('#screenshare video')?.remove();
    }

    if (this.track) {
      this.oldTrack = this.track;
      const el = this.track.attach();
      el.style.width = '100%';
      el.style.height = '100%';

      document.getElementById('screenshare')?.appendChild(el);
    }
  }
}
