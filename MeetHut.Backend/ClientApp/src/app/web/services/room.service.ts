import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RoomDTO } from '../dtos';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  constructor(private http: HttpClient) {}

  getAll(): Promise<RoomDTO[]> {
    return this.http.get<RoomDTO[]>(this.getRoomUrl('')).toPromise();
  }

  private getRoomUrl(endpoint: string): string {
    return `${environment.apiUrl}/Room/${endpoint}`;
  }
}
