import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TokenDTO } from '../dtos';
import { ForgotPasswordModel, LoginModel, RegistrationModel } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // eslint-disable-next-line @typescript-eslint/naming-convention
  private ACCESS_TOKEN_KEY = 'access-token';
  // eslint-disable-next-line @typescript-eslint/naming-convention
  private REFRESH_TOKEN_KEY = 'refresh-token';

  constructor(private http: HttpClient) {}

  get accessTokenExists(): boolean {
    return ![null, ''].includes(localStorage.getItem(this.ACCESS_TOKEN_KEY));
  }

  get accessToken(): string {
    return localStorage.getItem(this.ACCESS_TOKEN_KEY) ?? '';
  }

  private set accessToken(value: string) {
    if (value === '') {
      localStorage.removeItem(this.ACCESS_TOKEN_KEY);
    }

    localStorage.setItem(this.ACCESS_TOKEN_KEY, value);
  }

  get refreshToken(): string {
    return localStorage.getItem(this.REFRESH_TOKEN_KEY) ?? '';
  }

  private set refreshToken(value: string) {
    if (value === '') {
      localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    }

    localStorage.setItem(this.REFRESH_TOKEN_KEY, value);
  }

  login(model: LoginModel): Promise<TokenDTO> {
    return new Promise((resolve) =>
      this.http
        .post<TokenDTO>(this.getAuthUrl('login'), model)
        .toPromise()
        .then((res) => {
          this.saveTokens(res);
          resolve(res);
        })
        .catch((err) => console.error(err))
    );
  }

  register(model: RegistrationModel): Promise<void> {
    return this.http
      .post<void>(this.getAuthUrl('registration'), model)
      .toPromise();
  }

  forgotPassword(model: ForgotPasswordModel): Promise<void> {
    return this.http
      .post<void>(this.getAuthUrl('forgot-password'), model)
      .toPromise();
  }

  logout(): Promise<void> {
    return new Promise<void>((resolve) =>
      this.http
        .post<void>(this.getAuthUrl('logout'), {})
        .toPromise()
        .then(() => {
          this.clearTokens();
          resolve();
        })
    );
  }

  private getAuthUrl(endpoint: string): string {
    return `${environment.apiUrl}/Auth/${endpoint}`;
  }

  private saveTokens(tokens: TokenDTO): void {
    this.accessToken = tokens.accessToken;
    this.refreshToken = tokens.refreshToken;
  }

  private clearTokens(): void {
    this.accessToken = '';
    this.refreshToken = '';
  }
}
