import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { IRouteData } from '../../web-routing.module';

export interface IFrameSettings {
  showHeader: boolean;
  showFooter: boolean;
}

@Component({
  selector: 'app-frame',
  templateUrl: './frame.component.html',
  styleUrls: ['./frame.component.scss']
})
export class FrameComponent {
  public title = '';
  public showHeader = false;
  public showFooter = false;

  constructor(private router: Router, private activeRoute: ActivatedRoute) {
    this.router.events.subscribe((e) => {
      if (e instanceof NavigationEnd) {
        const routeData = this.activeRoute.firstChild?.snapshot
          .data as IRouteData;

        this.title = routeData?.title ?? '';
        this.showHeader = routeData?.frameSettings?.showHeader;
        this.showFooter = routeData?.frameSettings?.showFooter;
      }
    });
  }
}
