import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Participant } from '../../components/participant-tile/participant-tile.component';
import { RoomPublicDTO } from '../../dtos';
import { MeetRole } from '../../models/room-role.model';
import { RoomDTO } from '../../dtos';
import { RoomService } from '../../services';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss'],
  host: { class: 'frame-inner-component' }
})
export class RoomComponent implements OnInit {
  roomId?: string | null;
  connectToken?: string;

  roomOther?: RoomDTO;

  readonly groupAtSmallScreen = 6;
  readonly groupAtBigScreen = 12;

  // FIXME
  readonly participants: Participant[] = Array.from(Array(20).keys()).map(
    (k) =>
      new Participant(`P ${k + 1}`, k === 0 ? MeetRole.Lecturer : undefined)
  );

  readonly room: RoomPublicDTO;

  private _groupAt!: number;

  constructor(
    private activeRoute: ActivatedRoute,
    private route: ActivatedRoute,
    private roomService: RoomService
  ) {
    this.room = this.activeRoute.snapshot.data.room;
    this.setGrouping();
  }

  get isGroupingNeccessary(): boolean {
    return this.groupedParticipantCount > 0;
  }

  get groupedParticipantCount(): number {
    return this.participants.length - this.groupAt;
  }

  get groupAt(): number {
    return this._groupAt;
  }

  private set groupAt(value: number) {
    if (this._groupAt === value) {
      return;
    }

    this._groupAt = value;
  }

  ngOnInit(): void {}

  private setGrouping(): void {
    this.groupAt =
      window.innerWidth < 768 ? this.groupAtSmallScreen : this.groupAtBigScreen;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.setGrouping();
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.roomId = params.get('id');
      this.getRoom();
    });
  }

  getRoom() {
    if (!this.roomId) {
      return;
    }
    this.roomService.getPublicId(this.roomId).then((r) => {
      this.roomOther = r;
    });
  }

  connect() {
    if (!this.roomOther) {
      return;
    }
    this.roomService.connect(this.roomOther.id).then((o) => {
      this.connectToken = o.token;
    });
  }

  leave() {
    this.connectToken = undefined;
  }
}
