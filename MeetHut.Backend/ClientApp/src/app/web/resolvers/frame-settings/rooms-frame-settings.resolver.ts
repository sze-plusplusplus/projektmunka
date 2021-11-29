import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import {
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
    const controls: ControlSettings[] = [
      new ControlSettings({
        iconKey: 'add',
        open: () => of(window.alert('add button is pressed'))
      }),
      dashboardControl
    ];

    return new FrameSettings({
      showHeader: true,
      footerSettings: new FooterSettings({
        show: true,
        controls: controls
      })
    });
  }
}
