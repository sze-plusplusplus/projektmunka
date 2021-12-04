import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TokenService } from 'src/app/auth/services';
import { RoomDTO } from '../../dtos';
import { RoomUserDTO } from '../../dtos/roomuser.dto';
import { RoomService } from '../../services';

@Component({
  selector: 'app-participant-edit-dialog',
  templateUrl: './participant-edit-dialog.component.html',
  styleUrls: ['./participant-edit-dialog.component.scss']
})
export class ParticipantEditDialogComponent {
  participants?: RoomUserDTO[];
  userId!: number;
  userToAdd = '';

  constructor(
    public dialogRef: MatDialogRef<boolean>,
    public roomService: RoomService,
    tokenService: TokenService,
    @Inject(MAT_DIALOG_DATA) public data: RoomDTO
  ) {
    this.userId = tokenService.getUserId();
    roomService.getParticipants(data.id).then((p) => (this.participants = p));
  }

  add() {
    this.roomService.addParticipant(this.data.id, this.userToAdd).then((p) => {
      this.participants = p;
      this.userToAdd = '';
    });
  }

  remove(userId: number) {
    this.roomService
      .removeParticipant(this.data.id, userId)
      .then((p) => (this.participants = p));
  }

  close() {
    this.dialogRef.close();
  }
}
