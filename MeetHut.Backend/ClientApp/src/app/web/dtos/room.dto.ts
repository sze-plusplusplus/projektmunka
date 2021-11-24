export class RoomPublicDTO {
  name: string;
  publicId: string;

  constructor(name: string, publicId: string) {
    this.name = name;
    this.publicId = publicId;
  }
}

export class RoomDTO extends RoomPublicDTO {
  id: number;
  creation: Date;
  lastUpdate: Date;

  constructor(
    id: number,
    creation: Date,
    lastUpdate: Date,
    name: string,
    publicId: string
  ) {
    super(name, publicId);
    this.id = id;
    this.creation = creation;
    this.lastUpdate = lastUpdate;
  }
}
