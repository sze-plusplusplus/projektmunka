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

@NgModule({
  declarations: [
    FrameComponent,
    DashboardComponent,
    RoomsComponent,
    RoomComponent,
    UserComponent,
    HeaderComponent,
    FooterComponent
  ],
  imports: [CommonModule, HttpClientModule, WebRoutingModule, SharedModule],
  providers: [RoomService],
  exports: []
})
export class WebModule {}
