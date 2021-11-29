import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import {
  ControlLocation,
  ControlSettings,
  dashboardControl,
  FooterSettings,
  FrameSettings,
  FrameSettingsResolver,
  ToggleControlSettings
} from '../../models';

@Injectable({
  providedIn: 'root'
})
export class RoomFrameSettingsResolver extends FrameSettingsResolver {
  getSettings(): FrameSettings {
    const controls: ControlSettings[] = [
      // LEFT
      new ControlSettings({
        iconKey: 'settings',
        open: () => of(window.alert('settings button is pressed')),
        location: ControlLocation.LEFT
      }),
      // CENTER
      new ControlSettings({
        iconKey: 'mic',
        open: () =>
          of(
            window.alert(
              'The mic button is pressed (it is turned off from now on)'
            )
          ),
        toggleSettings: new ToggleControlSettings({
          iconKey: 'mic_off',
          color: 'warn',
          open: () =>
            of(
              window.alert(
                'The mic-off button is pressed (it is turned on from now on)'
              )
            )
        })
      }),
      new ControlSettings({
        iconKey: 'videocam',
        open: () =>
          of(
            window.alert(
              'The videocam button is pressed (it is turned off from now on)'
            )
          ),
        toggleSettings: new ToggleControlSettings({
          iconKey: 'videocam_off',
          color: 'warn',
          open: () =>
            of(
              window.alert(
                'The videocam-off button is pressed (it is turned on from now on)'
              )
            )
        })
      }),
      new ControlSettings({
        iconKey: 'screen_share',
        open: () =>
          of(
            window.alert(
              'The screen share button is pressed (it is turned off from now on)'
            )
          ),
        toggleSettings: new ToggleControlSettings({
          iconKey: 'stop_screen_share',
          color: 'warn',
          open: () =>
            of(
              window.alert(
                'The screen-share-off button is pressed (it is turned on from now on)'
              )
            )
        })
      }),
      new ControlSettings({
        iconKey: 'call_end',
        color: 'warn',
        open: () => of(window.alert('call end button is pressed'))
      }),
      // RIGHT
      new ControlSettings({
        iconKey: 'group',
        open: () =>
          of(
            window.alert(
              'The participants button is pressed (the dialog is opened from now on)'
            )
          ),
        location: ControlLocation.RIGHT,
        toggleSettings: new ToggleControlSettings({
          iconKey: 'group',
          open: () =>
            of(
              window.alert(
                'The participants-close button is pressed (the dialog is closed from now on)'
              )
            )
        })
      }),
      new ControlSettings({
        iconKey: 'message',
        open: () =>
          of(
            window.alert(
              'The chat button is pressed (the dialog is opened from now on)'
            )
          ),
        location: ControlLocation.RIGHT,
        toggleSettings: new ToggleControlSettings({
          iconKey: 'message',
          open: () =>
            of(
              window.alert(
                'The chat button is pressed (the dialog is closed from now on)'
              )
            )
        })
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
