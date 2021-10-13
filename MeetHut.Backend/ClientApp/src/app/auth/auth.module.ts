import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RegistrationComponent, LoginComponent } from './pages';
import { AuthGuard } from './guards';
import { AuthService, TokenService } from './services';

@NgModule({
  declarations: [LoginComponent, RegistrationComponent],
  imports: [CommonModule, FormsModule, HttpClientModule],
  providers: [AuthGuard, AuthService, TokenService],
  exports: [LoginComponent]
})
export class AuthModule {}
