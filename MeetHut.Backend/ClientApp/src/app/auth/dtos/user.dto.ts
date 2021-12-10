export class UserDTO {
  constructor(
    public id: number,
    public userName: string,
    public email: string,
    public fullName: string
  ) {}
}
