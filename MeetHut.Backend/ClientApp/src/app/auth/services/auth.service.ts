import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params, Router, RouterState } from '@angular/router';
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

  constructor(private http: HttpClient, private router: Router) {}

  /**
   * Check access token is in the local storage
   */
  get accessTokenExists(): boolean {
    return ![null, ''].includes(localStorage.getItem(this.ACCESS_TOKEN_KEY));
  }

  /**
   * Get access token from the local storage
   */
  get accessToken(): string {
    return localStorage.getItem(this.ACCESS_TOKEN_KEY) ?? '';
  }

  private set accessToken(value: string) {
    if (value === '') {
      localStorage.removeItem(this.ACCESS_TOKEN_KEY);
    }

    localStorage.setItem(this.ACCESS_TOKEN_KEY, value);
  }

  /**
   * Check refresh token is in the local storage
   */
  get refreshTokenExists(): boolean {
    return ![null, ''].includes(localStorage.getItem(this.REFRESH_TOKEN_KEY));
  }

  /**
   * Get access token from the local storage
   */
  get refreshToken(): string {
    return localStorage.getItem(this.REFRESH_TOKEN_KEY) ?? '';
  }

  private set refreshToken(value: string) {
    if (value === '') {
      localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    }

    localStorage.setItem(this.REFRESH_TOKEN_KEY, value);
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
        .post<TokenDTO>(this.getAuthUrl('login'), model)
        .toPromise()
        .then((res) => {
          this.saveTokens(res);
          resolve(res);
        })
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
      .post<void>(this.getAuthUrl('registration'), model)
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
      .post<void>(this.getAuthUrl('forgot-password'), model)
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
        .post<void>(this.getAuthUrl('logout'), {})
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
   * Navigate to home page
   */
  navigateToHome(): void {
    this.router.navigate(['home']);
  }

  /**
   * Navigate to the landing page
   */
  navigateToTheLandingPage(): void {
    this.router.navigate(['']);
  }

  /**
   * Navigate to the login page with query params possibility
   *
   * @param queryParams Quert params for redirection
   */
  navigateToTheLoginPage(queryParams: Params | null): void {
    this.router.navigate(['auth', 'login'], { queryParams });
  }

  navigateToTheLoginPageWithRoute(): void {
    this.navigateToTheLoginPage({
      redirect: this.router.routerState.snapshot.url
    });
  }

  private getAuthUrl(endpoint: string): string {
    return `${environment.apiUrl}/Auth/${endpoint}`;
  }
}
