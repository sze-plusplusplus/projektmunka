import { Injectable } from "@angular/core";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { environment } from "../../../environments/environment";
import { BehaviorSubject, Observable } from "rxjs";
import { Message } from "../models";

@Injectable({
  providedIn: "root"
})
export class ChatService {
  private _message = new BehaviorSubject<Message>(new Message('', '', '', new Date()));

  private _hubConnection: HubConnection;

  get message(): Observable<Message> {
    return this._message.asObservable();
  }

  constructor() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.appUrl}Chat`)
      .build();
    this.initListening();
    this.startConnection();
  }

  sendMessage(message: Message): void {
    this._hubConnection.invoke('NewMessage', message).then(() => console.log('Message sent!'));
  }

  private startConnection(): void {
    this._hubConnection.start()
      .then(() => {
        console.log('Connection created');
      })
      .catch(() => {
        console.log('Error during the connection...');
        setTimeout(() => this.startConnection(), 5000);
      });
  }

  private initListening(): void {
    this._hubConnection.on('MessageReceived', (data: Message) => {
      this._message.next(data);
    });
  }
}
