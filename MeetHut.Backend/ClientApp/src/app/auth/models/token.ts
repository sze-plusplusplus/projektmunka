export class Token {
  exp: number;

  constructor(expiration: number) {
    this.exp = expiration;
  }
}
