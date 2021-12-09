import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { ChatDialogComponent } from '../../components/dialog/chat-dialog/chat-dialog.component';
import { ParticipantsDialogComponent } from '../../components/dialog/participants-dialog/participants-dialog.component';
import { SettingsDialogComponent } from '../../components/dialog/settings-dialog/settings-dialog.component';
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
  readonly participantsControl: ControlSettings;
  readonly chatControl: ControlSettings;

  readonly micClickEvent: Subject<boolean> = new Subject();
  readonly videoCamClickEvent: Subject<boolean> = new Subject();
  readonly screenShareClickEvent: Subject<boolean> = new Subject();

  constructor(
    private route: ActivatedRoute,
    private roomService: RoomService,
    private dialog: MatDialog
  ) {
    const controls =
      this.route.snapshot.data.frameSettings.footerSettings.controls;

    // get controls
    this.micControl = controls.get(ControlId.Mic)!;
    this.videoCamControl = controls.get(ControlId.VideoCam)!;
    this.screenShareControl = controls.get(ControlId.ScreenShare)!;
    this.endCallControl = controls.get(ControlId.CallEnd)!;
    this.participantsControl = controls.get(ControlId.Participants)!;
    this.chatControl = controls.get(ControlId.Chat)!;

    // configure event handlers
    this.micControl.click.subscribe((v) => this.micClicked(v));
    this.videoCamControl.click.subscribe((v) => this.videoCamClicked(v));
    this.screenShareControl.click.subscribe((v) => this.screenShareClicked(v));
    this.endCallControl.click.subscribe(() => this.leave());
    this.participantsControl.click.subscribe(() =>
      this.openParticipantsDialog()
    );
    this.chatControl.click.subscribe(() => this.openChatDialog());
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

  // FIXME: NOT YET USED
  private openSettingsDialog(): Observable<void> {
    const dialogRef = this.dialog.open(SettingsDialogComponent, {
      width: '50vw',
      height: '60vh',
      panelClass: 'dialog'
    });

    return dialogRef.afterClosed();
  }

  private openParticipantsDialog(): Promise<void> {
    const dialogRef = this.dialog.open(ParticipantsDialogComponent, {
      data: this.room,
      width: '50vw',
      height: '80vh',
      panelClass: 'dialog'
    });

    return dialogRef
      .afterClosed()
      .toPromise()
      .then(() => this.participantsControl.toggle());
  }

  private openChatDialog(): Promise<void> {
    const dialogRef = this.dialog.open(ChatDialogComponent, {
      data: this.room,
      width: '50vw',
      height: '80vh',
      panelClass: 'dialog'
    });

    return dialogRef
      .afterClosed()
      .toPromise()
      .then(() => this.chatControl.toggle());
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
