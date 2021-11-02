export class RoomDTO {
  id: number;
  creation: Date;
  lastUpdate: Date;
  name: string;
  publicId: string;

  constructor(
    id: number,
    creation: Date,
    lastUpdate: Date,
    name: string,
    publicId: string
  ) {
    this.id = id;
    this.creation = creation;
    this.lastUpdate = lastUpdate;
    this.name = name;
    this.publicId = publicId;
  }
}
