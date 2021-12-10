import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-dialog-frame',
  templateUrl: './dialog-frame.component.html',
  styleUrls: ['./dialog-frame.component.scss']
})
export class DialogFrameComponent implements OnInit {
  @Input() title!: string;
  @Input() disableCLoseButton: boolean | undefined;

  @Output() close = new EventEmitter<void>();

  constructor() {}

  ngOnInit(): void {}

  closeAction(): void {
    this.close.emit();
  }
}
