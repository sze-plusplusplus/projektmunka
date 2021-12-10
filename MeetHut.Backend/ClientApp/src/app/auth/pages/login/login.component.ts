import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  GoogleLoginProvider,
  MicrosoftLoginProvider,
  SocialAuthService
} from 'angularx-social-login';
import { ParameterService } from 'src/app/shared/services';
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
  social = {
    google: false,
    microsoft: false
  };
  public loginModel: LoginModel = new LoginModel('', '');
  private redirectPath = '/dashboard';

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private socialAuthService: SocialAuthService,
    private parameterService: ParameterService
  ) {}

  /**
   * On Init hook
   */
  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      const redirectUrl = params.redirect;
      if (redirectUrl === '/auth/login') {
        this.redirectPath = '/dashboard';
      } else {
        this.redirectPath = redirectUrl || '/dashboard';
      }
    });
    this.parameterService.getAll().then((p) => {
      this.social.google = !!p.find((e) => e.key === 'Google.Login')?.value;
      this.social.microsoft = !!p.find((e) => e.key === 'Microsoft.Login')
        ?.value;
    });
  }

  /**
   * Do login
   * On success clears the state and navigates to dashboard or the redirect param
   */
  login() {
    this.authService
      .login(this.loginModel)
      .then(() => this.loginEvent())
      .catch((err) => console.log(err));
  }

  /**
   * Do login with Google
   * On success clears the state and navigates to dashboard or the redirect param
   */
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

  /**
   * Do login with Microsoft
   * On success clears the state and navigates to home or the redirect param
   */
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
