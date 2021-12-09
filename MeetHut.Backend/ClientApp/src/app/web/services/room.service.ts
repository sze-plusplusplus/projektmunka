import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { OpenDTO, RoomDTO } from '../dtos';
import { RoomUserDTO } from '../dtos/roomuser.dto';
import { RoomUserAddModel } from '../models/roomuseraddmodel';
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
    return this.http.get<RoomDTO[]>(RoomService.getRoomUrl('own')).toPromise();
  }

  getPublicId(publicId: string) {
    return this.http.get<RoomDTO>(RoomService.getRoomUrl(`publicId/${publicId}`));
  }

  connect(id: number): Promise<OpenDTO> {
    return this.http.get<OpenDTO>(RoomService.getRoomUrl(`${id}/open`)).toPromise();
  }

  deleteRoom(id: number) {
    return this.http.delete(RoomService.getRoomUrl(id.toString())).toPromise();
  }

  save(room: RoomDTO) {
    return this.http[room.id ? 'put' : 'post'](
      RoomService.getRoomUrl(room.id?.toString() || ''),
      this.onlySave(room)
    ).toPromise();
  }

  getParticipants(roomId: number) {
    return this.http
      .get<RoomUserDTO[]>(RoomService.getRoomUrl(`${roomId}/users`))
      .toPromise();
  }

  addParticipant(roomId: number, userNameOrEmail: string) {
    return this.http
      .put<RoomUserDTO[]>(RoomService.getRoomUrl(`${roomId}/users`), {
        userNameOrEmail
      } as RoomUserAddModel)
      .toPromise();
  }

  removeParticipant(roomId: number, userId: number) {
    return this.http
      .delete<RoomUserDTO[]>(RoomService.getRoomUrl(`${roomId}/users/${userId}`))
      .toPromise();
  }

  /**
   * Get calendar events
   *
   * @returns Promised room events
   */
  getCalendar(): Promise<RoomCalendarDTO[]> {
    return this.http.get<RoomCalendarDTO[]>(RoomService.getRoomUrl('calendar')).toPromise();
  }

  private onlySave(room: RoomDTO): RoomDTO {
    const tzoffset = new Date().getTimezoneOffset() * 60000;
    return {
      id: room.id,
      name: room.name,
      publicId: room.publicId || room.name,
      startTime: room.startTime
        ? new Date(new Date(room.startTime).valueOf() - tzoffset)
            .toISOString()
            .slice(0, -1)
        : null,
      endTime:
        room.startTime && room.endTime
          ? new Date(new Date(room.endTime).valueOf() - tzoffset)
              .toISOString()
              .slice(0, -1)
          : null
    } as RoomDTO;
  }
}
