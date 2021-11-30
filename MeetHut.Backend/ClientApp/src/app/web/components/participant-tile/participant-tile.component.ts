import { Component, Input, OnInit } from '@angular/core';
import { MeetRole } from '../../models/room-role.model';

export class Participant {
  fullName: string;
  meetRole: MeetRole;

  constructor(fullName: string, meetRole: MeetRole = MeetRole.Student) {
    this.fullName = fullName;
    this.meetRole = meetRole;
  }

  get monogram(): string {
    return this.fullName
      .split(' ')
      .map((n) => n[0])
      .join('');
  }
}

@Component({
  selector: 'app-participant-tile',
  templateUrl: './participant-tile.component.html',
  styleUrls: ['./participant-tile.component.scss']
})
export class ParticipantTileComponent implements OnInit {
  @Input() participant: Participant | undefined;
  @Input() groupCount: number = 0;

  private _showActionsButton: boolean = false;
  private readonly actionsButtonDisabled: boolean = true;

  constructor() {}

  get isLecturer(): boolean {
    return this.participant?.meetRole === MeetRole.Lecturer;
  }

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

    window.alert('actions of ' + this.participant!.fullName);
  }
}
