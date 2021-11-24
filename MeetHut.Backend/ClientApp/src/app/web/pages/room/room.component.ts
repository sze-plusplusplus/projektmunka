import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RoomPublicDTO } from '../../dtos';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit {
  readonly room: RoomPublicDTO;

  constructor(private activeRoute: ActivatedRoute) {
    this.room = this.activeRoute.snapshot.data.room;
  }

  ngOnInit(): void {}
}
