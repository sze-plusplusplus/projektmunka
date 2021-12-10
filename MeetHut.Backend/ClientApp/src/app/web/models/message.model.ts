export class Message {
  senderId: number;
  senderName: string;
  text: string;
  sendingTime: Date;

  constructor(senderId: number, senderName: string, text: string, sendingTime: Date) {
    this.senderId = senderId;
    this.senderName = senderName;
    this.text = text;
    this.sendingTime = sendingTime;
  }
}
