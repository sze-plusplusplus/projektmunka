import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from '.';
import { TokenDTO } from '../dto';

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

  private getTokenUrl(endpoint: string): string {
    return `${environment.apiUrl}/Token/${endpoint}`;
  }
}
