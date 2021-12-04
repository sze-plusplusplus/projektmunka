import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import {
  FrameComponent,
  ParticipantEditDialogComponent,
  RoomEditDialogComponent
} from './components';
import { DashboardComponent, RoomsComponent, RoomComponent } from './pages/';
import { RoomService } from './services';
import { WebRoutingModule } from './web-routing.module';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ParticipantTileComponent } from './components/participant-tile/participant-tile.component';
import { DialogFrameComponent } from './components/common/dialog-frame/dialog-frame.component';
import { ParticipantsDialogComponent } from './components/dialog/participants-dialog/participants-dialog.component';
import { ChatDialogComponent } from './components/dialog/chat-dialog/chat-dialog.component';
import { SettingsDialogComponent } from './components/dialog/settings-dialog/settings-dialog.component';
import { LivekitComponent } from './components/livekit/livekit.component';
import { VideoComponent } from './components/livekit/video/video.component';
import { ControlsComponent } from './components/livekit/controls/controls.component';
import { ScreenshareComponent } from './components/livekit/screenshare/screenshare.component';
import { ParticipantComponent } from './components/livekit/participant/participant.component';
import { MatListModule } from '@angular/material/list';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { NgxMatDatetimePickerModule } from '@angular-material-components/datetime-picker';

@NgModule({
  declarations: [
    FrameComponent,
    RoomEditDialogComponent,
    ParticipantEditDialogComponent,
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
    SettingsDialogComponent,
    LivekitComponent,
    VideoComponent,
    ControlsComponent,
    ScreenshareComponent,
    ParticipantComponent
  ],
  imports: [
    CommonModule,
    WebRoutingModule,
    SharedModule,
    FormsModule,
    MatListModule,
    MatTooltipModule,
    MatDialogModule,
    MatDatepickerModule,
    MatIconModule,
    NgxMatDatetimePickerModule
  ],
  providers: [RoomService],
  exports: []
})
export class WebModule {}
