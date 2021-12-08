import { Component, Input, OnInit } from '@angular/core';
import { Participant } from 'livekit-client';
import { MeetRole } from '../../models/room-role.model';

@Component({
  selector: 'app-participant-tile',
  templateUrl: './participant-tile.component.html',
  styleUrls: ['./participant-tile.component.scss']
})
export class ParticipantTileComponent implements OnInit {
  @Input() participant: Participant | undefined;
  @Input() groupCount = 0;

  private _showActionsButton = false;
  private readonly actionsButtonDisabled: boolean = true;

  constructor() {}

  get participantsText(): string {
    return `Participant${this.groupCount > 1 ? 's' : ''}`;
  }

  get showActionsButton(): boolean {
    if (this.actionsButtonDisabled) {
      return false;
    }

    return this._showActionsButton;
  }

  set showActionsButton(value: boolean) {
    if (this.actionsButtonDisabled) {
      return;
    }

    this._showActionsButton = value;
  }

  ngOnInit(): void {}

  openActionsPopup(): void {
    if (this.actionsButtonDisabled) {
      return;
    }

    window.alert('actions of ' + this.participant!.identity);
  }
}
