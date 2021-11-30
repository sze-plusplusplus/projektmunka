import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { FrameComponent } from './components';
import { DashboardComponent, RoomsComponent, UserComponent } from './pages/';
import { RoomComponent } from './pages/room/room.component';
import { RoomService } from './services';
import { WebRoutingModule } from './web-routing.module';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ParticipantTileComponent } from './components/participant-tile/participant-tile.component';
import { DialogFrameComponent } from './components/common/dialog-frame/dialog-frame.component';
import { ParticipantsDialogComponent } from './components/dialog/participants-dialog/participants-dialog.component';
import { ChatDialogComponent } from './components/dialog/chat-dialog/chat-dialog.component';

import { SettingsDialogComponent } from './components/dialog/settings-dialog/settings-dialog.component';

@NgModule({
  declarations: [
    FrameComponent,
    DashboardComponent,
    RoomsComponent,
    RoomComponent,
    UserComponent,
    HeaderComponent,
    FooterComponent,
    ParticipantTileComponent,
    ParticipantsDialogComponent,
    DialogFrameComponent,
    ChatDialogComponent,
    SettingsDialogComponent
  ],
  imports: [CommonModule, HttpClientModule, WebRoutingModule, SharedModule],
  providers: [RoomService],
  exports: []
})
export class WebModule {}
