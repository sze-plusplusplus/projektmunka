/* eslint-disable @typescript-eslint/consistent-type-assertions */
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/guards';
import { FrameComponent } from './components';
import { DashboardComponent, RoomsComponent, UserComponent, ChatComponent, RoomComponent } from './pages';
import { RoomFrameSettingsResolver } from './resolvers/frame-settings/room-frame-settings.resolver';
import { RoomsFrameSettingsResolver } from './resolvers/frame-settings/rooms-frame-settings.resolver';
import { RoomResolver } from './resolvers/room.resolver';
import { UserFrameSettingsResolver } from './resolvers/frame-settings/user-frame-settings.resolver';
import { TimeTableComponent } from './pages/time-table/time-table.component';

interface IRouteData {
  title?: string;
}

export class RouteData implements IRouteData {
  title: string;

  constructor(from?: IRouteData) {
    this.title = from?.title ?? '';
  }
}

const routes: Routes = [
  {
    path: '',
    component: FrameComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
        data: new RouteData({
          title: 'Dashboard'
        }),
        canActivate: [AuthGuard]
      },
      {
        path: 'rooms',
        component: RoomsComponent,
        resolve: {
          frameSettings: RoomsFrameSettingsResolver
        },
        data: new RouteData({
          title: 'Rooms'
        }),
        canActivate: [AuthGuard]
      },
      {
        path: 'user',
        component: UserComponent,
        data: {
          title: 'User'
        },
        resolve: {
          frameSettings: UserFrameSettingsResolver
        },
        canActivate: [AuthGuard]
      },
      {
        path: 'room/:id',
        component: RoomComponent,
        resolve: {
          room: RoomResolver,
          frameSettings: RoomFrameSettingsResolver
        },
        data: new RouteData({
          title: 'Room'
        }),
        canActivate: [AuthGuard]
      },
      {
        path: 'time-table',
        component: TimeTableComponent,
        data: new RouteData({
          title: 'Time Table'
        }),
        canActivate: [AuthGuard]
      },
      {
        path: 'demo-chat',
        component: ChatComponent,
        data: new RouteData({
          title: 'Chat'
        }),
        canActivate: [AuthGuard]
      },
      { path: '**', redirectTo: 'dashboard' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [RoomResolver]
})
export class WebRoutingModule {}
