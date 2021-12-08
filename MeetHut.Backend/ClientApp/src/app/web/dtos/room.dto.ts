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
  name: string;
  publicId: string;
  startTime: string;
  endTime: string;
  owner: {
    id: number;
    userName: string;
  };
  participants: number;
  onlineParticipants: number;

  constructor(
    id: number,
    creation: Date,
    lastUpdate: Date,
    name: string,
    publicId: string,
    startTime: string,
    endTime: string,
    owner: any,
    participants: number,
    onlineParticipants: number
  ) {
    super(name, publicId);
    this.id = id;
    this.creation = creation;
    this.lastUpdate = lastUpdate;
    this.name = name;
    this.publicId = publicId;
    this.startTime = startTime;
    this.endTime = endTime;
    this.owner = owner;
    this.participants = participants;
    this.onlineParticipants = onlineParticipants;
  }
}
