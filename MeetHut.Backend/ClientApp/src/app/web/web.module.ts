import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { FrameComponent } from './components';
import { DashboardComponent, RoomsComponent, UserComponent } from './pages/';
import { RoomService } from './services';
import { WebRoutingModule } from './web-routing.module';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { TimeTableComponent } from './pages/time-table/time-table.component';
import { ChatComponent } from './pages';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [FrameComponent, DashboardComponent, RoomsComponent, UserComponent, TimeTableComponent, ChatComponent],
  imports: [CommonModule, WebRoutingModule, SharedModule, CalendarModule.forRoot({
    provide: DateAdapter,
    useFactory: adapterFactory
  }), FormsModule, ReactiveFormsModule],
  providers: [RoomService],
  exports: []
})
export class WebModule {
}
