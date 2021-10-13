import { Component, OnInit } from '@angular/core';
import { RoomDTO } from '../../dtos';
import { RoomService } from '../../services';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.scss']
})
export class RoomsComponent implements OnInit {
  rooms: RoomDTO[] = [];

  constructor(private roomService: RoomService) {}

  ngOnInit(): void {
    this.getRooms();
  }

  private getRooms(): void {
    this.roomService.getAll().then((res) => (this.rooms = res));
  }
}
