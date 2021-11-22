import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/services';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
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
