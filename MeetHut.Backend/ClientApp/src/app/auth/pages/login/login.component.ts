import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  GoogleLoginProvider,
  MicrosoftLoginProvider,
  SocialAuthService
} from 'angularx-social-login';
import {
  GoogleLoginModel,
  LoginModel,
  MicrosoftLoginModel
} from '../../models';
import { AuthService } from '../../services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public loginModel: LoginModel = new LoginModel('', '');
  private redirectPath = '/home';

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private socialAuthService: SocialAuthService
  ) {}

  /**
   * On Init hook
   */
  ngOnInit(): void {
    this.route.queryParams.subscribe(
      (params) => (this.redirectPath = params.redirect || '/home')
    );
  }

  /**
   * Do login
   * On success clears the state and navigates to home or the redirect param
   */
  login() {
    this.authService
      .login(this.loginModel)
      .then(() => this.loginEvent())
      .catch((err) => console.log(err));
  }

  loginWithGoogle(): void {
    this.socialAuthService
      .signIn(GoogleLoginProvider.PROVIDER_ID)
      .then((res) => {
        this.authService
          .loginWithGoogle(new GoogleLoginModel(res.idToken, res.provider))
          .then(() => this.loginEvent())
          .catch((err) => {
            console.log(err);
            this.socialAuthService.signOut();
          });
      });
  }

  loginWithMicrosoft(): void {
    this.socialAuthService
      .signIn(MicrosoftLoginProvider.PROVIDER_ID)
      .then((res) => {
        this.authService
          .loginWithMicrosoft(
            new MicrosoftLoginModel(res.idToken, res.provider, res.authToken)
          )
          .then(() => this.loginEvent())
          .catch((err) => console.log(err));
      });
  }

  private loginEvent(): void {
    this.loginModel = new LoginModel('', '');
    this.router.navigateByUrl(this.redirectPath);
  }
}
