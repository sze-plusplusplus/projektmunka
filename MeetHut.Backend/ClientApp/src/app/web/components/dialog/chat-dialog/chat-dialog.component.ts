import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RoomDTO } from 'src/app/web/dtos';

@Component({
  selector: 'app-chat-dialog',
  templateUrl: './chat-dialog.component.html',
  styleUrls: ['./chat-dialog.component.scss'],
  host: {
    class: 'app-dialog'
  }
})
export class ChatDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<ChatDialogComponent, void>,
    @Inject(MAT_DIALOG_DATA) public data: RoomDTO
  ) {}

  ngOnInit(): void {}
}
