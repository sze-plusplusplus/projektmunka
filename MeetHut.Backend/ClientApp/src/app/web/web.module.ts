import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { FrameComponent } from './components';
import { DashboardComponent, RoomsComponent, UserComponent } from './pages/';
import { RoomService } from './services';
import { WebRoutingModule } from './web-routing.module';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { TimeTableComponent } from './pages/time-table/time-table.component';

@NgModule({
  declarations: [FrameComponent, DashboardComponent, RoomsComponent, UserComponent, TimeTableComponent],
  imports: [CommonModule, HttpClientModule, WebRoutingModule, SharedModule, CalendarModule.forRoot({
    provide: DateAdapter,
    useFactory: adapterFactory
  })],
  providers: [RoomService],
  exports: []
})
export class WebModule {
}
