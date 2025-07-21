import { ChangeDetectorRef, Component, ElementRef, forwardRef, Input, Renderer2 } from '@angular/core';

import { ToastComponent, ToasterService, ToastHeaderComponent, ToastBodyComponent, ToastCloseDirective, ProgressBarDirective, ProgressComponent, ToasterComponent, ButtonCloseDirective } from '@coreui/angular';

@Component({
  selector: 'app-notification',
  standalone: true,
  providers: [{ provide: ToastComponent, useExisting: forwardRef(() => NotificationComponent) }],
  imports: [ToastHeaderComponent, ToastBodyComponent, ToastCloseDirective, ProgressBarDirective, ProgressComponent,
    ToasterComponent, ToastComponent, ButtonCloseDirective
  ],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.scss'
})
export class NotificationComponent extends ToastComponent {

  @Input() closeButton = true;
  @Input() content = '';

  constructor(
    public override hostElement: ElementRef,
    public override renderer: Renderer2,
    public override toasterService: ToasterService,
    public override changeDetectorRef: ChangeDetectorRef
  ) {
    super(hostElement, renderer, toasterService, changeDetectorRef);
  }

  override ngOnInit(): void {
    console.log('NotificationComponent ngOnInit', this.color);
  }
}