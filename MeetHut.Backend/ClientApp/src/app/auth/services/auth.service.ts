import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { TokenDTO } from '../dtos';
import {
  ForgotPasswordModel,
  GoogleLoginModel,
  LoginModel,
  MicrosoftLoginModel,
  RegistrationModel
} from '../models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // eslint-disable-next-line @typescript-eslint/naming-convention
  private static readonly ACCESS_TOKEN_KEY = 'access-token';
  // eslint-disable-next-line @typescript-eslint/naming-convention
  private static readonly REFRESH_TOKEN_KEY = 'refresh-token';

  constructor(private http: HttpClient, private router: Router) {}

  /**
   * Check access token is in the local storage
   */
  get accessTokenExists(): boolean {
    return ![null, ''].includes(
      localStorage.getItem(AuthService.ACCESS_TOKEN_KEY)
    );
  }

  /**
   * Check refresh token is in the local storage
   */
  get refreshTokenExists(): boolean {
    return ![null, ''].includes(
      localStorage.getItem(AuthService.REFRESH_TOKEN_KEY)
    );
  }

  /**
   * Get access token from the local storage
   */
  get accessToken(): string {
    return localStorage.getItem(AuthService.ACCESS_TOKEN_KEY) ?? '';
  }

  private set accessToken(value: string) {
    if (value === '') {
      localStorage.removeItem(AuthService.ACCESS_TOKEN_KEY);
    }

    localStorage.setItem(AuthService.ACCESS_TOKEN_KEY, value);
  }

  /**
   * Get access token from the local storage
   */
  get refreshToken(): string {
    return localStorage.getItem(AuthService.REFRESH_TOKEN_KEY) ?? '';
  }

  private set refreshToken(value: string) {
    if (value === '') {
      localStorage.removeItem(AuthService.REFRESH_TOKEN_KEY);
    }

    localStorage.setItem(AuthService.REFRESH_TOKEN_KEY, value);
  }

  private static getAuthUrl(endpoint: string): string {
    return `${environment.apiUrl}/Auth/${endpoint}`;
  }

  /**
   * Calls login endpoint
   * Saves the result token on success
   *
   * @param model Login input
   * @returns Promised tokens
   */
  login(model: LoginModel): Promise<TokenDTO> {
    return new Promise((resolve) =>
      this.http
        .post<TokenDTO>(AuthService.getAuthUrl('login'), model)
        .toPromise()
        .then((res) => res && this.handleTokens(res, resolve))
        .catch((err) => console.error(err))
    );
  }

  /**
   * Calls login with google endpoint
   * Saves the result token on success
   *
   * @param model Google login input
   * @returns Promised tokens
   */
  loginWithGoogle(model: GoogleLoginModel): Promise<TokenDTO> {
    return new Promise((resolve) =>
      this.http
        .post<TokenDTO>(AuthService.getAuthUrl('google-login'), model)
        .toPromise()
        .then((res) => res && this.handleTokens(res, resolve))
        .catch((err) => console.error(err))
    );
  }

  /**
   * Calls login with microsoft endpoint
   * Saves the result token on success
   *
   * @param model Microsoft login input
   * @returns Promised tokens
   */
  loginWithMicrosoft(model: MicrosoftLoginModel): Promise<TokenDTO> {
    return new Promise((resolve) =>
      this.http
        .post<TokenDTO>(AuthService.getAuthUrl('ms-login'), model)
        .toPromise()
        .then((res) => res && this.handleTokens(res, resolve))
        .catch((err) => console.error(err))
    );
  }

  /**
   * Calls register endpoint
   *
   * @param model Registration input
   * @returns Promise event
   */
  register(model: RegistrationModel): Promise<void> {
    return this.http
      .post<void>(AuthService.getAuthUrl('registration'), model)
      .toPromise();
  }

  /**
   * Calls forgot password endpoint
   *
   * @param model Forgot password input
   * @returns Promise event
   */
  forgotPassword(model: ForgotPasswordModel): Promise<void> {
    return this.http
      .post<void>(AuthService.getAuthUrl('forgot-password'), model)
      .toPromise();
  }

  /**
   * Logout
   * Clear the tokens from the clien side
   * Do logout event on the API side
   *
   * @param action Success action
   * @returns Promise event
   */
  logout(action?: () => void): Promise<void> {
    return new Promise<void>((resolve) =>
      this.http
        .post<void>(AuthService.getAuthUrl('logout'), {})
        .toPromise()
        .then(() => {
          this.clearTokens();
          if (action) {
            action();
          }
          resolve();
        })
    );
  }

  /**
   * Clear tokens from the local storage
   */
  clearTokens(): void {
    this.accessToken = '';
    this.refreshToken = '';
  }

  /**
   * Saves tokens into the local storage
   *
   * @param tokens Tokens
   */
  saveTokens(tokens: TokenDTO): void {
    this.accessToken = tokens.accessToken;
    this.refreshToken = tokens.refreshToken;
  }

  /**
   * Navigate to dashboard page
   */
  navigateToDashboard(): void {
    this.router.navigate(['dashboard']);
  }

  /**
   * Navigate to the login page with query params possibility
   *
   * @param queryParams Quert params for redirection
   */
  navigateToTheLoginPage(queryParams: Params | null): void {
    this.router.navigate(['auth', 'login'], { queryParams });
  }

  /**
   * Navigate to the login page with the current route
   */
  navigateToTheLoginPageWithRoute(): void {
    const url = this.router.routerState.snapshot.url;
    if (url !== '/auth/login') {
      this.navigateToTheLoginPage({
        redirect: url
      });
    }
  }

  private handleTokens(
    tokens: TokenDTO,
    resolve: (value: TokenDTO | PromiseLike<TokenDTO>) => void
  ): void {
    this.saveTokens(tokens);
    resolve(tokens);
  }
}
