import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import jwtDecode from 'jwt-decode';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from '.';
import { TokenDTO } from '../dtos';
import { Token } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  /**
   * Refresh access token by refresh token
   *
   * @returns Promised tokens
   */
  refresh(): Promise<TokenDTO> {
    return this.refreshObservable().toPromise();
  }

  /**
   * Refresh access token by refresh token
   *
   * @returns Observable refresh
   */
  refreshObservable(): Observable<TokenDTO> {
    return this.http.post<TokenDTO>(this.getTokenUrl('refresh'), {
      accessToken: this.authService.accessToken,
      refreshToken: this.authService.refreshToken
    });
  }

  /**
   * Get access token's expiration date from the token
   *
   * @returns Expiration date
   */
  getExpirationDate(): Date {
    const token = this.authService.accessToken;
    if (!token) {
      return new Date(-8640000000000000);
    }

    const obj = jwtDecode<Token>(token);
    return new Date(obj.exp * 1000);
  }

  getUserId() {
    const token = this.authService.accessToken;
    if (!token) {
      return -1;
    }

    const obj = jwtDecode<Token>(token);
    return Number(
      obj[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ]
    );
  }

  /**
   * Check access token is expired
   *
   * @returns True when expired
   */
  tokenIsExpired(): boolean {
    return this.getExpirationDate() <= new Date();
  }

  private getTokenUrl(endpoint: string): string {
    return `${environment.apiUrl}/Token/${endpoint}`;
  }
}
