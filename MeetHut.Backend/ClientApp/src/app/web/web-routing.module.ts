import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/guards';
import {
  FrameComponent,
  IFrameSettings
} from "./components";
import { ChatComponent, DashboardComponent, RoomsComponent, UserComponent } from "./pages";

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
        path: 'demo-chat',
        component: ChatComponent,
        data: <IRouteData>{
          title: 'Chat',
          frameSettings: <IFrameSettings>{ showHeader: true, showFooter: true }
        }
      },
      { path: '**', redirectTo: 'dashboard' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WebRoutingModule {}
