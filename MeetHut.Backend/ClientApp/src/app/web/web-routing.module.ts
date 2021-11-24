import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/guards';
import { FrameComponent, IFrameSettings } from './components';
import { DashboardComponent, RoomsComponent, UserComponent } from './pages';
import { RoomComponent } from './pages/room/room.component';
import { RoomResolver } from './resolvers/room.resolver';

export interface IRouteData {
  title: string;
  frameSettings: IFrameSettings;
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
        data: <IRouteData>{
          title: 'Dashboard',
          frameSettings: <IFrameSettings>{
            showHeader: false,
            showFooter: false
          }
        },
        canActivate: [AuthGuard]
      },
      {
        path: 'rooms',
        component: RoomsComponent,
        data: <IRouteData>{
          title: 'Rooms',
          frameSettings: <IFrameSettings>{ showHeader: true, showFooter: true }
        },
        canActivate: [AuthGuard]
      },
      {
        path: 'user',
        component: UserComponent,
        data: <IRouteData>{
          title: 'User',
          frameSettings: <IFrameSettings>{ showHeader: true, showFooter: true }
        }
      },
      {
        path: 'room/:publicId',
        component: RoomComponent,
        resolve: {
          room: RoomResolver
        },
        data: <IRouteData>{
          title: 'Room',
          frameSettings: <IFrameSettings>{ showHeader: true, showFooter: true }
        },
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
