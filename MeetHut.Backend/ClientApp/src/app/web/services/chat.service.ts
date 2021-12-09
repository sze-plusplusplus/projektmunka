/* eslint-disable no-underscore-dangle */
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { Message } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private _message = new BehaviorSubject<Message>(new Message(-1, '', '', new Date()));

  private _hubConnection: HubConnection;

  get message(): Observable<Message> {
    return this._message.asObservable();
  }

  constructor() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.appUrl}Chat`)
      .build();
    this.initListening();
    // this.startConnection(); // You need to call by hand
  }

  sendMessage(publicId: string, message: Message): Promise<void> {
    return this._hubConnection.invoke('NewMessage', publicId, message).then(() => console.log('Message sent!'));
  }

  connectToGroup(publicId: string): void {
    this._hubConnection.invoke('AddToGroup', publicId).then(() => console.log('Connected to the group'));
  }

  disconnectFromGroup(publicId: string): void {
    this._hubConnection.invoke('RemoveFromGroup', publicId).then(() => console.log('Disconnected from the group'));
  }

  startConnection(initAction?: () => void): Promise<void> {
    return new Promise((resolve) => {
      this._hubConnection.start()
        .then(() => {
          console.log('Connection created');
          if (initAction) {
            initAction();
          }

          resolve();
        })
        .catch(() => {
          console.log('Error during the connection...');
          setTimeout(() => this.startConnection(initAction), 5000);
        });
    });
  }

  private initListening(): void {
    this._hubConnection.on('MessageReceived', (data: Message) => {
      console.log(data);
      this._message.next(data);
    });
  }
}
