import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/guards';
import {
  FrameComponent,
  IFrameSettings
} from './components/frame/frame.component';
import { DashboardComponent, RoomsComponent } from './pages';

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
      { path: '**', redirectTo: 'dashboard' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WebRoutingModule {}
