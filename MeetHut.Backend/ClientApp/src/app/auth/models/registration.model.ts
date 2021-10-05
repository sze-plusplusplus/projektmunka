export class RegistrationModel {
  userName: string;
  password: string;
  email: string;
  fullName: string;

  constructor(
    userName: string,
    password: string,
    email: string,
    fullName: string
  ) {
    this.userName = userName;
    this.password = password;
    this.email = email;
    this.fullName = fullName;
  }
}
