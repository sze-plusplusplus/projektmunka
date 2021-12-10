import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RoomDTO } from 'src/app/web/dtos';

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
    public dialogRef: MatDialogRef<SettingsDialogComponent, void>,
    @Inject(MAT_DIALOG_DATA) public data: RoomDTO
  ) {}
  ngOnInit(): void {}
}
