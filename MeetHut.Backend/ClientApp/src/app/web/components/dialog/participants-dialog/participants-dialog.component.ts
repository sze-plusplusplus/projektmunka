import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RoomDTO } from 'src/app/web/dtos';
import { RoomUserDTO } from 'src/app/web/dtos/roomuser.dto';
import { RoomService } from 'src/app/web/services';

@Component({
  selector: 'app-participants-dialog',
  templateUrl: './participants-dialog.component.html',
  styleUrls: ['./participants-dialog.component.scss'],
  host: {
    class: 'app-dialog'
  }
})
export class ParticipantsDialogComponent implements OnInit {
  users: RoomUserDTO[] = [];

  constructor(
    public dialogRef: MatDialogRef<ParticipantsDialogComponent, void>,
    private roomService: RoomService,
    @Inject(MAT_DIALOG_DATA) public data: RoomDTO
  ) {}

  ngOnInit(): void {
    this.roomService
      .getParticipants(this.data.id)
      .then((ru) => ru && (this.users = ru));
  }
}
