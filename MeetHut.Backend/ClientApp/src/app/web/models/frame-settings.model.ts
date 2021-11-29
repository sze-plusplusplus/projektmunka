import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot
} from '@angular/router';
import { FooterSettings } from '.';

interface IFrameSettings {
  showHeader?: boolean;
  footerSettings?: FooterSettings;
}

export class FrameSettings implements IFrameSettings {
  showHeader: boolean;
  footerSettings: FooterSettings;

  constructor(from?: IFrameSettings) {
    this.showHeader = from?.showHeader ?? false;
    this.footerSettings = from?.footerSettings ?? new FooterSettings();
  }
}

export abstract class FrameSettingsResolver implements Resolve<FrameSettings> {
  abstract getSettings(): FrameSettings;

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): FrameSettings {
    return this.getSettings();
  }
}
