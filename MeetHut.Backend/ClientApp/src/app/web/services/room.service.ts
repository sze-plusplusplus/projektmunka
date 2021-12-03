import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { OpenDTO, RoomDTO } from '../dtos';

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
    return this.http.get<RoomDTO[]>(this.getRoomUrl('own')).toPromise();
  }

  getPublicId(publicId: string): Promise<RoomDTO> {
    return this.http
      .get<RoomDTO>(this.getRoomUrl(`publicId/${publicId}`))
      .toPromise();
  }

  connect(id: number): Promise<OpenDTO> {
    return this.http.get<OpenDTO>(this.getRoomUrl(`open/${id}`)).toPromise();
  }

  private getRoomUrl(endpoint: string): string {
    return `${environment.apiUrl}/Room/${endpoint}`;
  }
}
