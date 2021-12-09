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
  private _connectionState = new BehaviorSubject<boolean>(false);
  private _messages = new BehaviorSubject<Message[]>([]);

  private _hubConnection: HubConnection;

  constructor() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.appUrl}Chat`)
      .build();
    this.initListening();
    this.startConnection();
  }

  get state(): Observable<boolean> {
    return this._connectionState.asObservable();
  }

  get messages(): Observable<Message[]> {
    return this._messages.asObservable();
  }

  sendMessage(publicId: string, message: Message): Promise<void> {
    return this._hubConnection
      .invoke('NewMessage', publicId, message)
      .then(() => console.log('Message sent!'));
  }

  connectToGroup(publicId: string): Promise<void> {
    return this._hubConnection
      .invoke('AddToGroup', publicId)
      .then(() => console.log('Connected to the group'));
  }

  disconnectFromGroup(publicId: string): Promise<void> {
    return this._hubConnection
      .invoke('RemoveFromGroup', publicId)
      .then(() => console.log('Disconnected from the group'));
  }

  startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        console.log('Connection created');
        this._connectionState.next(true);
      })
      .catch(() => {
        console.log('Error during the connection...');
        setTimeout(() => this.startConnection(), 5000);
      });
  }

  private initListening(): void {
    this._hubConnection.on('MessageReceived', (data: Message) => {
      if (data) {
        this._messages.next([...this._messages.value, data]);
      }
    });
  }
}
