/* eslint-disable @typescript-eslint/consistent-type-assertions */
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/guards';
import { FrameComponent } from './components';
import {
  DashboardComponent,
  RoomsComponent,
  UserComponent,
  RoomComponent,
  TimeTableComponent
} from './pages';
import { RoomResolver } from './resolvers';
import {
  RoomFrameSettingsResolver,
  RoomsFrameSettingsResolver,
  TimeTableFrameSettingsResolver,
  UserFrameSettingsResolver
} from './resolvers/frame-settings';

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
        path: '',
        pathMatch: 'full',
        redirectTo: 'dashboard'
      },
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
        resolve: {
          frameSettings: TimeTableFrameSettingsResolver
        },
        data: new RouteData({
          title: 'Time Table'
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
