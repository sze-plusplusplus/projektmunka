export class Message {
  clientId: string;
  senderId: string;
  text: string;
  sendingTime: Date;

  constructor(clientId: string, senderId: string, text: string, sendingTime: Date) {
    this.clientId = clientId;
    this.senderId = senderId;
    this.text = text;
    this.sendingTime = sendingTime;
  }
}
