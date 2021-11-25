import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { FrameComponent } from './components';
import { DashboardComponent, RoomsComponent, UserComponent } from './pages/';
import { RoomService } from './services';
import { WebRoutingModule } from './web-routing.module';

@NgModule({
  declarations: [FrameComponent, DashboardComponent, RoomsComponent, UserComponent],
  imports: [CommonModule, HttpClientModule, WebRoutingModule, SharedModule],
  providers: [RoomService],
  exports: []
})
export class WebModule {}
