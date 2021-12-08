import { Component, OnDestroy, OnInit } from "@angular/core";
import { ChatService } from "../../services";
import { Subscription } from "rxjs";
import { Message } from "../../models";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, OnDestroy {
  private _subs: Subscription = new Subscription();

  messages: Message[] = [];
  currMessage: string = '';

  constructor(private chatService: ChatService) { }

  ngOnInit(): void {
    this._subs.add(this.chatService.message.subscribe(res => {
      if (res != null) {
        this.messages.push(res);
      }
    }));
  }

  ngOnDestroy(): void {
    this._subs.unsubscribe();
  }

  sendMessage(): void {
    this.chatService.sendMessage(new Message('', '', this.currMessage, new Date()));
  }
}
