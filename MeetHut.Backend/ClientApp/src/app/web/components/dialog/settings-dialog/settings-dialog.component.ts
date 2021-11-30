import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ParticipantsDialogComponent } from '../participants-dialog/participants-dialog.component';

@Component({
  selector: 'app-settings-dialog',
  templateUrl: './settings-dialog.component.html',
  styleUrls: ['./settings-dialog.component.scss'],
  host: {
    class: 'app-dialog'
  }
})
export class SettingsDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<ParticipantsDialogComponent, void>
  ) {}
  ngOnInit(): void {}
}
