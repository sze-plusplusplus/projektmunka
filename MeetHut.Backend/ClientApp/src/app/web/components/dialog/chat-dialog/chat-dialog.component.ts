/* eslint-disable @angular-eslint/no-host-metadata-property */
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { UserDTO, RoomDTO } from 'src/app/web/dtos';
import { Message } from 'src/app/web/models';
import { ChatService, UserService } from 'src/app/web/services';

@Component({
  selector: 'app-chat-dialog',
  templateUrl: './chat-dialog.component.html',
  styleUrls: ['./chat-dialog.component.scss'],
  host: {
    class: 'app-dialog'
  }
})
export class ChatDialogComponent implements OnInit, OnDestroy {
  messages: Message[] = [];
  currMessage = '';
  publicId = '';
  user?: UserDTO;

  private subs: Subscription = new Subscription();

  constructor(
    public dialogRef: MatDialogRef<ChatDialogComponent, void>,
    @Inject(MAT_DIALOG_DATA) data: RoomDTO,
    private chatService: ChatService,
    userService: UserService
  ) {
    userService.getCurrent().then((res) => (this.user = res));
    this.publicId = data.publicId;
  }

  ngOnInit(): void {
    this.subs.add(
      this.chatService.messages.subscribe((res) => {
        if (res) {
          this.messages = res;
        }
      })
    );
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  sendMessage(): void {
    if (
      this.user &&
      this.publicId &&
      this.currMessage &&
      this.currMessage.trim().length > 0
    ) {
      this.chatService
        .sendMessage(
          this.publicId,
          new Message(
            this.user.id,
            this.user.fullName ? this.user.fullName : this.user.userName,
            this.currMessage,
            new Date()
          )
        )
        .then(() => (this.currMessage = ''));
    }
  }
}
