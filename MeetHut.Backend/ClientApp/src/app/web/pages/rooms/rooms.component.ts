import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TokenService } from 'src/app/auth/services';
import {
  ParticipantEditDialogComponent,
  RoomEditDialogComponent
} from '../../components';
import { RoomDTO } from '../../dtos';
import { RoomService } from '../../services';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.scss']
})
export class RoomsComponent implements OnInit {
  rooms: RoomDTO[] = [];
  userId!: number;

  constructor(
    private roomService: RoomService,
    private tokenService: TokenService,
    public dialog: MatDialog
  ) {}

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
      data: room
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
      } as RoomDTO
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
      data: room
    });
  }

  private getRooms(): void {
    this.roomService.getAll().then((res) => (this.rooms = res));
  }
}
