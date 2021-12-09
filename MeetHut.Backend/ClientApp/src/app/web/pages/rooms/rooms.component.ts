import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { TokenService } from 'src/app/auth/services';
import {
  ParticipantEditDialogComponent,
  RoomEditDialogComponent
} from '../../components';
import { RoomDTO } from '../../dtos';
import { ControlArray, ControlId, ControlSettings } from '../../models';
import { RoomService } from '../../services';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.scss']
})
export class RoomsComponent implements OnInit {
  rooms: RoomDTO[] = [];
  userId!: number;

  readonly addControl: ControlSettings;

  constructor(
    private roomService: RoomService,
    private tokenService: TokenService,
    public dialog: MatDialog,
    private route: ActivatedRoute
  ) {
    const controls: ControlArray = this.route.snapshot.data.frameSettings
      .footerSettings.controls;

    this.addControl = controls.get(ControlId.Add)!;
    this.addControl.click.subscribe(() => this.createRoom());
  }

  /**
   * On Init hook
   */
  ngOnInit(): void {
    this.getRooms();
    this.userId = this.tokenService.getUserId();
  }

  deleteRoom(id: number) {
    this.roomService.deleteRoom(id).then(() => this.getRooms());
  }

  editRoom(room: RoomDTO) {
    const ref = this.dialog.open(RoomEditDialogComponent, {
      data: room,
      panelClass: 'dialog'
    });

    ref.afterClosed().subscribe((res) => {
      if (res) {
        this.roomService.save(res).then(() => this.getRooms());
      }
    });
  }

  createRoom() {
    const ref = this.dialog.open(RoomEditDialogComponent, {
      data: {
        name: 'New room'
      } as RoomDTO,
      panelClass: 'dialog'
    });

    ref.afterClosed().subscribe((res) => {
      if (res) {
        this.roomService.save(res).then(() => this.getRooms());
      }
    });
  }

  participantsDialog(room: RoomDTO) {
    this.dialog.open(ParticipantEditDialogComponent, {
      minWidth: '50vw',
      data: room,
      panelClass: 'dialog'
    });
  }

  private getRooms(): void {
    this.roomService.getAll().then((res) => (this.rooms = res));
  }
}
