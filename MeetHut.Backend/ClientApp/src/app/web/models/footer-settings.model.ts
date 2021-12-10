import { ControlArray } from '.';

interface IFooterSettings {
  show?: boolean;
  controls?: ControlArray;
}

export class FooterSettings implements IFooterSettings {
  show: boolean;
  controls: ControlArray;

  constructor(from?: IFooterSettings) {
    this.show = from?.show ?? false;
    this.controls = new ControlArray(from?.controls?.items ?? []);
  }
}
