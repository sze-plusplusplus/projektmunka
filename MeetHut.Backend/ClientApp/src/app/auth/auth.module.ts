/* eslint-disable @typescript-eslint/naming-convention */
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RegistrationComponent, LoginComponent } from './pages';
import { AuthGuard } from './guards';
import { AuthService, TokenService } from './services';
import {
  GoogleLoginProvider,
  MicrosoftLoginProvider,
  SocialAuthServiceConfig,
  SocialLoginModule
} from 'angularx-social-login';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [LoginComponent, RegistrationComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    SocialLoginModule,
    SharedModule
  ],
  providers: [
    AuthGuard,
    AuthService,
    TokenService,
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '384354348458-oempt2jkiujkr7gcm7l9v0aru48bgp94.apps.googleusercontent.com'
            )
          },
          {
            id: MicrosoftLoginProvider.PROVIDER_ID,
            provider: new MicrosoftLoginProvider(
              '29ee6459-8ea6-4556-a487-e97f511832b8',
              {
                redirect_uri: 'https://localhost:5001/signin-ms'
              }
            )
          }
        ]
      } as SocialAuthServiceConfig
    }
  ],
  exports: [LoginComponent]
})
export class AuthModule {}
