import { Router } from '@angular/router';
import { Observable, of, Subject } from 'rxjs';

export enum ControlLocation {
  LEFT = 0,
  CENTER = 1,
  RIGHT = 2
}

export class ControlArray {
  items: ControlSettings[];

  constructor(items?: ControlSettings[]) {
    this.items = items ?? [];
  }

  get(id: ControlId): ControlSettings | undefined {
    return this.items.find((i) => i.id === id);
  }

  withControl(control: IControlSettings): ControlArray {
    this.items.push(new ControlSettings(control));
    return this;
  }
}

export interface IToggleControlSettings {
  iconKey?: string;
  color?: 'primary' | 'warn' | 'accent';

  routeTo?: string;
  open?: () => Observable<any>;
  // TODO: overview
}

export class ToggleControlSettings implements IToggleControlSettings {
  iconKey?: string;
  color?: 'primary' | 'warn' | 'accent';

  routeTo?: string;
  open?: () => Observable<any>;
  click: Subject<boolean>;

  constructor(from: IToggleControlSettings) {
    this.iconKey = from.iconKey;
    this.color = from.color ?? 'accent';
    this.routeTo = from.routeTo;
    this.open = from.open;
    this.click = new Subject();
  }
}

export enum ControlId {
  Settings = 1,
  Mic = 2,
  VideoCam = 3,
  ScreenShare = 4,
  CallEnd = 5,
  Participants = 6,
  Chat = 7,
  Add = 8,
  Dashboard = 9
}
export interface IControlSettings extends IToggleControlSettings {
  id: ControlId;
  iconKey: string;
  color?: 'primary' | 'warn' | 'accent';
  location?: ControlLocation;
  toggled?: boolean;

  toggleSettings?: ToggleControlSettings;
}

export class ControlSettings implements IControlSettings {
  id: ControlId;

  iconKey: string;
  color: 'primary' | 'warn' | 'accent';
  location: ControlLocation;
  toggled: boolean;

  routeTo?: string;
  open?: () => Observable<any>;
  click: Subject<any>;
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
    this.toggled = !this.toggled;
  }

  constructor(from: IControlSettings) {
    this.id = from.id;
    this.iconKey = from.iconKey;
    this.color = from.color ?? 'primary';
    this.location = from.location ?? ControlLocation.CENTER;
    this.routeTo = from.routeTo;
    this.open = from.open;
    this.click = new Subject();
    this.toggleSettings = from.toggleSettings;
    this.toggled = from.toggled ?? false;

    if (this.toggled) {
      this.toggle();
    }
  }
}

export const dashboardControl = new ControlSettings({
  id: ControlId.Dashboard,
  iconKey: 'dashboard',
  color: 'accent',
  routeTo: 'dashboard',
  location: ControlLocation.RIGHT
});
