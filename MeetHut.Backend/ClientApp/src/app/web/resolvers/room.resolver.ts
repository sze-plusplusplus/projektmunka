import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { RoomDTO } from '../dtos';
import { RoomService } from '../services';

@Injectable({
  providedIn: 'root'
})
export class RoomResolver implements Resolve<RoomDTO> {
  constructor(private roomService: RoomService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<RoomDTO> {
    const publicId = route.params.id;
    const room = this.roomService.getPublicId(publicId);
    room.subscribe((r) => {
      const routeData = { ...route.data };
      // update only title
      routeData.title = `${route.data.title} - ${r.name} - #${r.publicId}`;
      route.data = routeData;

      return r;
    });

    return room;
  }
}
