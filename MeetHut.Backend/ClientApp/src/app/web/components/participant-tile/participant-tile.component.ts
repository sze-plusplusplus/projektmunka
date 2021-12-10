import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { LocalParticipant, Participant } from 'livekit-client';

@Component({
  selector: 'app-participant-tile',
  templateUrl: './participant-tile.component.html',
  styleUrls: ['./participant-tile.component.scss']
})
export class ParticipantTileComponent implements OnInit {
  @Input() participant: Participant | undefined;
  @Input() groupCount = 0;

  @Output() settingsOpen = new EventEmitter<void>();

  private _showActionsButton = true;
  private readonly actionsButtonDisabled: boolean = true;

  constructor() {}

  get participantsText(): string {
    return `Participant${this.groupCount > 1 ? 's' : ''}`;
  }

  get showActionsButton(): boolean {
    if (this.actionsButtonDisabled && !this.isMe) {
      return false;
    }

    return this._showActionsButton;
  }

  set showActionsButton(value: boolean) {
    if (this.actionsButtonDisabled && !this.isMe) {
      return;
    }

    this._showActionsButton = value;
  }

  get isMe() {
    return this.participant instanceof LocalParticipant;
  }

  ngOnInit(): void {}

  openActionsPopup(): void {
    if (this.actionsButtonDisabled) {
      return;
    }

    window.alert('actions of ' + this.participant!.identity);
  }
}
