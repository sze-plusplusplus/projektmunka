import { Component, OnInit } from '@angular/core';
import { LoginModel } from '../../models';
import { AuthService } from '../../services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public loginModel: LoginModel = new LoginModel('', '');

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  login() {
    this.authService
      .login(this.loginModel)
      .then(() => {
        this.loginModel = new LoginModel('', '');
      })
      .catch((err) => console.log(err));
  }
}
