import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ParticipantsDialogComponent } from '../participants-dialog/participants-dialog.component';

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
    public dialogRef: MatDialogRef<ParticipantsDialogComponent, void>
  ) {}

  ngOnInit(): void {}
}
