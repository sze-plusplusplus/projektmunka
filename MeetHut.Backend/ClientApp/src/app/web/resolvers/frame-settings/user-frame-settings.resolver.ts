import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
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
export class UserFrameSettingsResolver extends FrameSettingsResolver {
  constructor(private dialog: MatDialog, private route: ActivatedRoute) {
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
