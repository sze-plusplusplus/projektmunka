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

  refresh(): Promise<TokenDTO> {
    return this.refreshObservable().toPromise();
  }

  refreshObservable(): Observable<TokenDTO> {
    return this.http.post<TokenDTO>(this.getTokenUrl('refresh'), {
      accessToken: this.authService.accessToken,
      refreshToken: this.authService.refreshToken
    });
  }

  getExpirationDate(): Date {
    const token = this.authService.accessToken;
    if (!token) {
      return new Date(-8640000000000000);
    }

    const obj = jwtDecode<Token>(token);
    return new Date(obj.exp * 1000);
  }

  tokenIsExpired(): boolean {
    return this.getExpirationDate() <= new Date();
  }

  private getTokenUrl(endpoint: string): string {
    return `${environment.apiUrl}/Token/${endpoint}`;
  }
}
