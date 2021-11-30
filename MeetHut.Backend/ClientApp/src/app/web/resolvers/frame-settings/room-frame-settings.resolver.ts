import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { ChatDialogComponent } from '../../components/dialog/chat-dialog/chat-dialog.component';
import { ParticipantsDialogComponent } from '../../components/dialog/participants-dialog/participants-dialog.component';
import { SettingsDialogComponent } from '../../components/dialog/settings-dialog/settings-dialog.component';
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
  constructor(private dialog: MatDialog) {
    super();
  }

  getSettings(): FrameSettings {
    const controls: ControlSettings[] = [
      // LEFT
      new ControlSettings({
        iconKey: 'settings',
        open: () => this.openSettingsDialog(),
        toggleSettings: new ToggleControlSettings({}),
        location: ControlLocation.LEFT
      }),
      // CENTER
      new ControlSettings({
        iconKey: 'mic',
        toggleSettings: new ToggleControlSettings({
          iconKey: 'mic_off',
          color: 'warn'
        })
      }),
      new ControlSettings({
        iconKey: 'videocam',
        toggleSettings: new ToggleControlSettings({
          iconKey: 'videocam_off',
          color: 'warn'
        })
      }),
      new ControlSettings({
        iconKey: 'screen_share',
        toggleSettings: new ToggleControlSettings({
          iconKey: 'stop_screen_share',
          color: 'warn'
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
        open: () => this.openParticipantsDialog(),
        toggleSettings: new ToggleControlSettings({}),
        location: ControlLocation.RIGHT
      }),
      new ControlSettings({
        iconKey: 'message',
        open: () => this.openChatDialog(),
        toggleSettings: new ToggleControlSettings({}),
        location: ControlLocation.RIGHT
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

  private openSettingsDialog(): Observable<void> {
    const dialogRef = this.dialog.open(SettingsDialogComponent, {
      width: '50vw',
      height: '60vh',
      panelClass: 'dialog'
    });

    return dialogRef.afterClosed();
  }

  private openParticipantsDialog(): Observable<void> {
    const dialogRef = this.dialog.open(ParticipantsDialogComponent, {
      width: '50vw',
      height: '80vh',
      panelClass: 'dialog'
    });

    return dialogRef.afterClosed();
  }

  private openChatDialog(): Observable<void> {
    const dialogRef = this.dialog.open(ChatDialogComponent, {
      width: '50vw',
      height: '80vh',
      panelClass: 'dialog'
    });

    return dialogRef.afterClosed();
  }
}
