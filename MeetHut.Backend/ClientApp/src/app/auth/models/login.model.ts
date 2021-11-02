export class LoginModel {
  userName: string;
  password: string;

  constructor(userName: string, password: string) {
    this.userName = userName;
    this.password = password;
  }
}

export class GoogleLoginModel {
  idToken: string;
  provider: string;

  constructor(idToken: string, provider: string) {
    this.idToken = idToken;
    this.provider = provider;
  }
}

export class MicrosoftLoginModel {
  idToken: string;
  provider: string;
  authToken: string;

  constructor(idToken: string, provider: string, authToken: string) {
    this.idToken = idToken;
    this.provider = provider;
    this.authToken = authToken;
  }
}
