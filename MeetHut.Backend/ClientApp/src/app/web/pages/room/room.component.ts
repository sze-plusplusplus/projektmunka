import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RoomDTO } from '../../dtos';
import { RoomService } from '../../services';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit {
  connectToken?: string;

  room?: RoomDTO;

  constructor(
    private route: ActivatedRoute,
    private roomService: RoomService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((r) => {
      this.room = (r as { room: RoomDTO }).room;
      console.log(r);
    });
  }

  connect() {
    if (!this.room) {
      return;
    }
    this.roomService.connect(this.room.id).then((o) => {
      this.connectToken = o.token;
      console.log(o.token);
    });
  }

  leave() {
    this.connectToken = undefined;
  }
}
