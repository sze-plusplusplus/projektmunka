import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/guards';
import { FrameComponent } from './components';
import { DashboardComponent, RoomsComponent } from './pages';
import { RoomComponent } from './pages/room/room.component';
import { RoomFrameSettingsResolver } from './resolvers/frame-settings/room-frame-settings.resolver';
import { RoomsFrameSettingsResolver } from './resolvers/frame-settings/rooms-frame-settings.resolver';
import { RoomResolver } from './resolvers/room.resolver';

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
        path: 'room/:publicId',
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
