import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { RoomDTO } from '../../dtos';
import { ControlId, ControlSettings } from '../../models';
import { RoomService } from '../../services';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit {
  connectToken?: string;

  room?: RoomDTO;

  readonly micControl: ControlSettings;
  readonly videoCamControl: ControlSettings;
  readonly screenShareControl: ControlSettings;
  readonly endCallControl: ControlSettings;

  readonly micClickEvent: Subject<boolean> = new Subject();
  readonly videoCamClickEvent: Subject<boolean> = new Subject();
  readonly screenShareClickEvent: Subject<boolean> = new Subject();

  constructor(private route: ActivatedRoute, private roomService: RoomService) {
    const controls =
      this.route.snapshot.data.frameSettings.footerSettings.controls;

    // get controls
    this.micControl = controls.get(ControlId.Mic)!;
    this.videoCamControl = controls.get(ControlId.VideoCam)!;
    this.screenShareControl = controls.get(ControlId.ScreenShare)!;
    this.endCallControl = controls.get(ControlId.CallEnd)!;

    // configure event handlers
    this.micControl.click.subscribe((v) => this.micClicked(v));
    this.videoCamControl.click.subscribe((v) => this.videoCamClicked(v));
    this.screenShareControl.click.subscribe((v) => this.screenShareClicked(v));
    this.endCallControl.click.subscribe(() => this.leave());
  }

  private micClicked(v: boolean): void {
    this.micClickEvent.next(v);
  }

  private videoCamClicked(v: boolean): void {
    this.videoCamClickEvent.next(v);
  }

  private screenShareClicked(v: boolean): void {
    this.screenShareClickEvent.next(v);
  }

  ngOnInit(): void {
    this.route.data.subscribe((r) => {
      this.room = (r as { room: RoomDTO }).room;
      console.log(r);
    });
  }

  connect(): void {
    if (!this.room) {
      return;
    }
    this.roomService.connect(this.room.id).then((o) => {
      this.connectToken = o.token;
      console.log(o.token);
    });
  }

  leave(): void {
    this.connectToken = undefined;
  }
}
