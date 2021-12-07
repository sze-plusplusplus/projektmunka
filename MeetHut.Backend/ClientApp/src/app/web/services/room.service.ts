import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RoomDTO } from '../dtos';
import { RoomCalendarDTO } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(private http: HttpClient) {}

  private static getRoomUrl(endpoint: string): string {
    return `${environment.apiUrl}/Room/${endpoint}`;
  }

  /**
   * Get all room entry
   *
   * @returns Promised rooms
   */
  getAll(): Promise<RoomDTO[]> {
    return this.http.get<RoomDTO[]>(RoomService.getRoomUrl('')).toPromise();
  }

  /**
   * Get calendar events
   *
   * @returns Promised room events
   */
  getCalendar(): Promise<RoomCalendarDTO[]> {
    return this.http.get<RoomCalendarDTO[]>(RoomService.getRoomUrl('calendar')).toPromise();
  }
}
