import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent, RoomsComponent } from './pages/';
import { RoomService } from './services';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [HomeComponent, RoomsComponent],
  imports: [CommonModule, HttpClientModule],
  providers: [RoomService],
  exports: []
})
export class WebModule {}
