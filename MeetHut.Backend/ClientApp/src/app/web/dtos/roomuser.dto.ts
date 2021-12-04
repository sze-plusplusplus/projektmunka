import { UserDTO } from 'src/app/auth/dtos/user.dto';
import { MeetRole } from '../models/meetrole';

export class RoomUserDTO {
  constructor(
    public userId: number,
    public user: UserDTO,
    public roomId: number,
    public role: MeetRole,
    public added: string
  ) {}
}
