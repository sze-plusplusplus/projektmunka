import { Component, OnInit } from '@angular/core';
import { UserDTO } from '../../dtos';
import { UserService } from '../../services';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  user?: UserDTO;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.getUser();
  }

  private getUser(): void {
    this.userService.getCurrent().then((res) => (this.user = res));
  }
}
