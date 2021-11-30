import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ControlLocation, FooterSettings } from '../../models';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {
  @Input() settings: FooterSettings | undefined;

  controlsExpanded: boolean = false;

  readonly leftLocation = ControlLocation.LEFT;
  readonly centerLocation = ControlLocation.CENTER;
  readonly rightLocation = ControlLocation.RIGHT;

  constructor(public router: Router) {
    this.setExpandedState();
  }

  ngOnInit(): void {}

  private setExpandedState(): void {
    if (window.innerWidth >= 768) {
      this.controlsExpanded = true;
    }
  }
}
