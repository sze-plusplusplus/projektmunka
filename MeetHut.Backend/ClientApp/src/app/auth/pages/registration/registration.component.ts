import { Component, OnInit } from '@angular/core';
import { RegistrationModel } from '../../models';
import { AuthService } from '../../services';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  public registrationModel: RegistrationModel = new RegistrationModel(
    '',
    '',
    '',
    ''
  );

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  /**
   * Do registration
   * On success clear the state
   */
  registration(): void {
    this.authService.register(this.registrationModel).then(() => {
      this.registrationModel = new RegistrationModel('', '', '', '');
    });
  }
}
