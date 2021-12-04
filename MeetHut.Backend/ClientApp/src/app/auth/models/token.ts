export class Token {
  exp: number;
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier': string;

  constructor(expiration: number, id: string) {
    this.exp = expiration;
    this[
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
    ] = id;
  }
}
