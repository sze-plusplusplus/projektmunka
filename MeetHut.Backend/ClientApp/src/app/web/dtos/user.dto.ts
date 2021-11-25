export class UserDTO {
  id: number;
  userName: string;
  email: string;
  fullName: string;
  lastLogin: Date;

  constructor(
    id: number,
    userName: string,
    email: string,
    fullName: string,
    lastLogin: Date
  ) {
    this.id = id;
    this.userName = userName;
    this.email = email;
    this.fullName = fullName;
    this.lastLogin = lastLogin;
  }
}
