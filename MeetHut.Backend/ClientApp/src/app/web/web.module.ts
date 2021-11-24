import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { FrameComponent } from './components';
import { DashboardComponent, RoomsComponent } from './pages/';
import { RoomComponent } from './pages/room/room.component';
import { RoomService } from './services';
import { WebRoutingModule } from './web-routing.module';

@NgModule({
  declarations: [
    FrameComponent,
    DashboardComponent,
    RoomsComponent,
    RoomComponent
  ],
  imports: [CommonModule, HttpClientModule, WebRoutingModule, SharedModule],
  providers: [RoomService],
  exports: []
})
export class WebModule {}
