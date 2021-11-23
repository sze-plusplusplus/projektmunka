import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserDTO } from '../dtos';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) {}

  private static getUserUrl(endpoint: string): string {
    return `${environment.apiUrl}/User/${endpoint}`;
  }

  /**
   * Get current user by token
   *
   * @returns User object
   */
  getCurrent(): Promise<UserDTO> {
    return this.http.get<UserDTO>(UserService.getUserUrl('me')).toPromise();
  }

  /**
   * Get user by id
   *
   * @param id User's Id
   * @returns User objects
   */
  get(id: number): Promise<UserDTO> {
    return this.http
      .get<UserDTO>(UserService.getUserUrl(id.toString()))
      .toPromise();
  }

  /**
   * Get all user
   *
   * @returns List of users
   */
  getAll(): Promise<UserDTO[]> {
    return this.http.get<UserDTO[]>(UserService.getUserUrl('')).toPromise();
  }
}
