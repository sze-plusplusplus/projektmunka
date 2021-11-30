import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-participants-dialog',
  templateUrl: './participants-dialog.component.html',
  styleUrls: ['./participants-dialog.component.scss'],
  host: {
    class: 'app-dialog'
  }
})
export class ParticipantsDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<ParticipantsDialogComponent, void>
  ) {}

  ngOnInit(): void {}
}
