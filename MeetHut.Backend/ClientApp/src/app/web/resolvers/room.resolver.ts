import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { RoomPublicDTO } from '../dtos';

@Injectable({
  providedIn: 'root'
})
export class RoomResolver implements Resolve<RoomPublicDTO> {
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<RoomPublicDTO> {
    const publicId = route.params.publicId;
    // TOOD: get from remote
    const room = new RoomPublicDTO('Conference 4', publicId);

    let routeData = { ...route.data };
    // update only title
    routeData.title = `${route.data.title} - ${room.name} - #${room.publicId}`;
    route.data = routeData;

    return of(room);
  }
}
