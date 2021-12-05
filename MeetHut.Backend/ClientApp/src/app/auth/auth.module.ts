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
import { ParameterService } from '../shared/services';

const socialConfigFactory = (parameterService: ParameterService) =>
  new Promise((resolve) => {
    parameterService.getAll().then((res) => {
      const providers = [];

      if (res) {
        // Google
        const googleClientIdParam = res.find(
          (x) => x.key === 'Google.ClientId'
        );
        if (googleClientIdParam && googleClientIdParam.value) {
          providers.push({
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(googleClientIdParam.value)
          });
        }

        // Microsoft
        const msClientIdParam = res.find((x) => x.key === 'Microsoft.ClientId');
        const msRedirectUri = res.find((x) => x.key === 'Microsoft.RedirectUri');
        if (msClientIdParam && msClientIdParam.value && msRedirectUri && msRedirectUri.value) {
          providers.push({
            id: MicrosoftLoginProvider.PROVIDER_ID,
            provider: new MicrosoftLoginProvider(msClientIdParam.value, {
              redirect_uri: msRedirectUri.value
            })
          });
        }
      }

      resolve({
        autoLogin: false,
        providers
      } as SocialAuthServiceConfig);
    });
  });

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
      useFactory: socialConfigFactory,
      deps: [ParameterService]
    }
  ],
  exports: [LoginComponent]
})
export class AuthModule {}
