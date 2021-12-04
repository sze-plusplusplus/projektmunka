import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RoomDTO } from '../../dtos';

@Component({
  selector: 'app-room-edit-dialog',
  templateUrl: './room-edit-dialog.component.html',
  styleUrls: ['./room-edit-dialog.component.scss']
})
export class RoomEditDialogComponent {
  room!: any;

  constructor(
    public dialogRef: MatDialogRef<RoomDTO>,
    @Inject(MAT_DIALOG_DATA) public data: RoomDTO
  ) {
    this.room = { ...data };
  }

  close() {
    this.dialogRef.close();
  }
}
