import { Component, OnDestroy, OnInit } from '@angular/core';
import { ChatService, UserService } from '../../services';
import { Subscription } from 'rxjs';
import { Message } from '../../models';
import { UserDTO } from '../../dtos';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, OnDestroy {
  messages: Message[] = [];
  currMessage = '';
  publicId = 'abc-123';
  user?: UserDTO;

  private subs: Subscription = new Subscription();

  constructor(private chatService: ChatService, userService: UserService)
  {
    userService.getCurrent().then((res) => this.user = res);
  }

  ngOnInit(): void {
    this.chatService.startConnection(() => this.chatService.connectToGroup(this.publicId)).then(() => {
      this.subs.add(this.chatService.message.subscribe(res => {
        if (res != null && res.senderId !== -1) {
          this.messages.push(res);
        }
      }));
    });
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
    this.chatService.disconnectFromGroup(this.publicId);
  }

  sendMessage(): void {
    if (this.user) {
      this.chatService.sendMessage(
        this.publicId,
        new Message(this.user.id, this.user.fullName ? this.user.fullName : this.user.userName, this.currMessage, new Date()))
        .then(() => this.currMessage = '');
    }
  }
}
