import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { RouteData } from '../web-routing.module';

export enum ControlLocation {
  LEFT = 0,
  CENTER = 1,
  RIGHT = 2
}

export interface IToggleControlSettings {
  iconKey?: string;
  color?: 'primary' | 'warn' | 'accent';

  routeTo?: string;
  open?: () => Observable<any>;
  // TODO: other actions
}

export class ToggleControlSettings implements IToggleControlSettings {
  iconKey?: string;
  color?: 'primary' | 'warn' | 'accent';

  routeTo?: string;
  open?: () => Observable<any>;

  constructor(from: IToggleControlSettings) {
    this.iconKey = from.iconKey;
    this.color = from.color ?? 'accent';
    this.routeTo = from.routeTo;
    this.open = from.open;
  }
}

export interface IControlSettings extends IToggleControlSettings {
  iconKey: string;
  color?: 'primary' | 'warn' | 'accent';
  location?: ControlLocation;

  toggleSettings?: ToggleControlSettings;
}

export class ControlSettings implements IControlSettings {
  iconKey: string;
  color: 'primary' | 'warn' | 'accent';
  location: ControlLocation;

  routeTo?: string;
  open?: () => Observable<any>;
  toggleSettings?: ToggleControlSettings;

  get action(): (router?: Router) => Promise<any> {
    let a: Observable<any>[] = [];

    if (this.routeTo) {
      // FIXME
      return (r) => Promise.resolve(r?.navigate([this.routeTo]));
    }

    if (this.open) {
      a.push(
        of(
          this.open()
            .toPromise()
            .then(() => this.toggle())
        )
      );
    }

    if (this.toggleSettings) {
      a.push(of(this.toggle()));
    }

    return () => Promise.all(a.map((i) => i.toPromise()));
  }

  private toggle(): void {
    const to = { ...this, ...this.toggleSettings } as ControlSettings;
    this.toggleSettings = { ...this } as ControlSettings;

    this.iconKey = to.iconKey ?? this.iconKey;
    this.color = to.color;
    this.routeTo = to.routeTo;
    this.open = to.open;
  }

  constructor(from: IControlSettings) {
    this.iconKey = from.iconKey;
    this.color = from.color ?? 'primary';
    this.location = from.location ?? ControlLocation.CENTER;
    this.routeTo = from.routeTo;
    this.open = from.open;
    this.toggleSettings = from.toggleSettings;
  }
}

export const dashboardControl = new ControlSettings({
  iconKey: 'dashboard',
  color: 'accent',
  routeTo: 'dashboard',
  location: ControlLocation.RIGHT
});
