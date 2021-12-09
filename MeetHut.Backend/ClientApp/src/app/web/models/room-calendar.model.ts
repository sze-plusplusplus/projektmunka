export class RoomCalendarDTO {
  id: number;
  name: string;
  publicId: string;
  startTime: Date;
  endTime?: Date;

  isOwner: boolean;
  isLocked: boolean;

  constructor(id: number, name: string, publicId: string, startTime: Date, endtime?: Date, isOwner?: boolean, isLocked?: boolean) {
    this.id = id;
    this.name = name;
    this.publicId = publicId;
    this.startTime = startTime;
    this.endTime = endtime;
    this.isOwner = isOwner ?? false;
    this.isLocked = isLocked ?? false;
  }
}
