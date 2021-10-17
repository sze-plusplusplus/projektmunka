import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  constructor(private authService: AuthService) {}

  /**
   * On Init hook
   */
  ngOnInit(): void {}

  /**
   * Logout from the app
   */
  logout(): void {
    this.authService
      .logout(() => this.authService.navigateToTheLandingPage())
      .then();
  }
}
