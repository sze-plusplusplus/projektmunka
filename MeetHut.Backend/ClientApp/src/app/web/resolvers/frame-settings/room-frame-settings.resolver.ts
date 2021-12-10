import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import {
  ControlArray,
  ControlId,
  ControlLocation,
  FooterSettings,
  FrameSettings,
  FrameSettingsResolver,
  ToggleControlSettings
} from '../../models';

@Injectable({
  providedIn: 'root'
})
export class RoomFrameSettingsResolver extends FrameSettingsResolver {
  constructor(private dialog: MatDialog, private route: ActivatedRoute) {
    super();
  }

  getSettings(): FrameSettings {
    const controls: ControlArray = new ControlArray()
      // LEFT
      /*.withControl({
        id: ControlId.Settings,
        iconKey: 'settings',
        toggleSettings: new ToggleControlSettings({}),
        location: ControlLocation.LEFT
      })*/
      // CENTER
      .withControl({
        id: ControlId.Mic,
        iconKey: 'mic',
        toggleSettings: new ToggleControlSettings({
          iconKey: 'mic_off',
          color: 'warn'
        }),
        toggled: true
      })
      .withControl({
        id: ControlId.VideoCam,
        iconKey: 'videocam',
        toggleSettings: new ToggleControlSettings({
          iconKey: 'videocam_off',
          color: 'warn'
        }),
        toggled: true
      })
      .withControl({
        id: ControlId.ScreenShare,
        iconKey: 'screen_share',
        toggleSettings: new ToggleControlSettings({
          iconKey: 'stop_screen_share',
          color: 'warn'
        }),
        toggled: true
      })
      .withControl({
        id: ControlId.CallEnd,
        iconKey: 'call_end',
        color: 'warn'
      })
      // RIGHT
      .withControl({
        id: ControlId.Participants,
        iconKey: 'group',
        toggleSettings: new ToggleControlSettings({}),
        location: ControlLocation.RIGHT
      })
      .withControl({
        id: ControlId.Chat,
        iconKey: 'message',
        toggleSettings: new ToggleControlSettings({}),
        location: ControlLocation.RIGHT
      });

    return new FrameSettings({
      showHeader: true,
      footerSettings: new FooterSettings({
        show: true,
        controls
      })
    });
  }
}
