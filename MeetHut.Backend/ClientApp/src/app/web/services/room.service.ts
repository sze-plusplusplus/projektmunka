import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment';
import { OpenDTO, RoomDTO } from '../dtos';
import { RoomUserDTO } from '../dtos/roomuser.dto';
import { RoomUserAddModel } from '../models/roomuseraddmodel';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  constructor(private http: HttpClient) {}

  /**
   * Get all room entry
   *
   * @returns Promised rooms
   */
  getAll(): Promise<RoomDTO[]> {
    return firstValueFrom(this.http.get<RoomDTO[]>(this.getRoomUrl('own')));
  }

  getPublicId(publicId: string) {
    return this.http.get<RoomDTO>(this.getRoomUrl(`publicId/${publicId}`));
  }

  connect(id: number): Promise<OpenDTO> {
    return firstValueFrom(
      this.http.get<OpenDTO>(this.getRoomUrl(`${id}/open`))
    );
  }

  deleteRoom(id: number) {
    return this.http.delete(this.getRoomUrl(id.toString())).toPromise();
  }

  save(room: RoomDTO) {
    return this.http[room.id ? 'put' : 'post'](
      this.getRoomUrl(room.id?.toString() || ''),
      this.onlySave(room)
    ).toPromise();
  }

  getParticipants(roomId: number) {
    return this.http
      .get<RoomUserDTO[]>(this.getRoomUrl(`${roomId}/users`))
      .toPromise();
  }

  addParticipant(roomId: number, userNameOrEmail: string) {
    return this.http
      .put<RoomUserDTO[]>(this.getRoomUrl(`${roomId}/users`), {
        userNameOrEmail
      } as RoomUserAddModel)
      .toPromise();
  }

  removeParticipant(roomId: number, userId: number) {
    return this.http
      .delete<RoomUserDTO[]>(this.getRoomUrl(`${roomId}/users/${userId}`))
      .toPromise();
  }

  private getRoomUrl(endpoint: string): string {
    return `${environment.apiUrl}/Room/${endpoint}`;
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
