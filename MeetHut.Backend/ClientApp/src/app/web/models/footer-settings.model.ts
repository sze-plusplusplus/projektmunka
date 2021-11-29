import { ControlSettings } from '.';

interface IFooterSettings {
  show?: boolean;
  controls?: ControlSettings[];
}

export class FooterSettings implements IFooterSettings {
  show: boolean;
  controls: ControlSettings[];

  constructor(from?: IFooterSettings) {
    this.show = from?.show ?? false;
    this.controls = from?.controls ?? [];
  }
}
