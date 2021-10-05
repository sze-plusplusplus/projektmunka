import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ForgotPasswordModel, LoginModel, RegistrationModel } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private $token: BehaviorSubject<string> = new BehaviorSubject<string>('');

  constructor(private http: HttpClient) {}

  get tokenExists(): boolean {
    return this.$token.getValue() !== '';
  }

  get token(): string {
    return this.$token.getValue();
  }

  login(model: LoginModel): Promise<string> {
    return new Promise((resolve) =>
      this.http
        .post<string>(this.getAuthUrl('login'), model)
        .toPromise()
        .then((res) => {
          this.$token.next(res);
          resolve(res);
        })
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

  private getAuthUrl(endpoint: string): string {
    return `${environment.apiUrl}/Auth/${endpoint}`;
  }
}
