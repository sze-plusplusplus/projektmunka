import { Injectable } from '@angular/core';
import {
  ControlArray,
  dashboardControl,
  FooterSettings,
  FrameSettings,
  FrameSettingsResolver
} from '../../models';

@Injectable({
  providedIn: 'root'
})
export class TimeTableFrameSettingsResolver extends FrameSettingsResolver {
  constructor() {
    super();
  }

  getSettings(): FrameSettings {
    const controls: ControlArray = new ControlArray([dashboardControl]);

    return new FrameSettings({
      showHeader: true,
      footerSettings: new FooterSettings({
        show: true,
        controls
      })
    });
  }
}
