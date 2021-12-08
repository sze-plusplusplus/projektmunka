import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ChatDialogComponent } from '../../components/dialog/chat-dialog/chat-dialog.component';
import { ParticipantsDialogComponent } from '../../components/dialog/participants-dialog/participants-dialog.component';
import { SettingsDialogComponent } from '../../components/dialog/settings-dialog/settings-dialog.component';
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
  constructor(private dialog: MatDialog) {
    super();
  }

  getSettings(): FrameSettings {
    const controls: ControlArray = new ControlArray()
      // LEFT
      .withControl({
        id: ControlId.Settings,
        iconKey: 'settings',
        open: () => this.openSettingsDialog(),
        toggleSettings: new ToggleControlSettings({}),
        location: ControlLocation.LEFT
      })
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
        open: () => this.openParticipantsDialog(),
        toggleSettings: new ToggleControlSettings({}),
        location: ControlLocation.RIGHT
      })
      .withControl({
        id: ControlId.Chat,
        iconKey: 'message',
        open: () => this.openChatDialog(),
        toggleSettings: new ToggleControlSettings({}),
        location: ControlLocation.RIGHT
      });

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
