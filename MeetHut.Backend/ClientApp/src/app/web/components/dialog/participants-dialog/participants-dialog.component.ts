import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
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
    public roomService: RoomService
  ) {}

  ngOnInit(): void {
    this.roomService.getParticipants(2).then((ru) => ru && (this.users = ru));
  }
}
