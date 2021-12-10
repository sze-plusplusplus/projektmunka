import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { FooterSettings } from '../../models/footer-settings.model';
import { RouteData } from '../../web-routing.module';

@Component({
  selector: 'app-frame',
  templateUrl: './frame.component.html',
  styleUrls: ['./frame.component.scss']
})
export class FrameComponent {
  public title = '';
  public showHeader = false;
  public footerSettings!: FooterSettings;

  constructor(private router: Router, private activeRoute: ActivatedRoute) {
    this.router.events.subscribe((e) => {
      if (e instanceof NavigationEnd) {
        const routeData = this.activeRoute.firstChild?.snapshot.data;

        this.title = routeData?.title ?? '';
        this.showHeader = routeData?.frameSettings?.showHeader ?? false;
        this.footerSettings = routeData?.frameSettings?.footerSettings ??  new FooterSettings();
      }
    });
  }
}
