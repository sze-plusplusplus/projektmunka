import { Injectable } from '@angular/core';
import {
  ControlArray,
  ControlId,
  ControlSettings,
  dashboardControl,
  FooterSettings,
  FrameSettings,
  FrameSettingsResolver
} from '../../models';

@Injectable({
  providedIn: 'root'
})
export class RoomsFrameSettingsResolver extends FrameSettingsResolver {
  getSettings(): FrameSettings {
    const controls: ControlArray = new ControlArray([
      new ControlSettings({
        id: ControlId.Add,
        iconKey: 'add'
      }),
      dashboardControl
    ]);

    return new FrameSettings({
      showHeader: true,
      footerSettings: new FooterSettings({
        show: true,
        controls: controls
      })
    });
  }
}
