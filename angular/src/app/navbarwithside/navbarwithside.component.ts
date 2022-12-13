import {eLayoutType, SubscriptionService} from '@abp/ng.core';
import { collapseWithMargin, slideFromBottom } from '@abp/ng.theme.shared';
import {AfterViewInit, Component} from '@angular/core';
import { LayoutService } from '../../lib/services/layout.service';

@Component({
  selector: 'app-navbarwithside',
  templateUrl: './navbarwithside.component.html',
  styleUrls: ['./navbarwithside.component.scss'],
  animations: [slideFromBottom, collapseWithMargin],
  providers: [LayoutService, SubscriptionService],
})
export class NavbarwithsideComponent implements AfterViewInit {
  // required for dynamic component
  static type = eLayoutType.application;

  constructor(public service: LayoutService) {}

  ngAfterViewInit() {
    this.service.subscribeWindowSize();
  }

}
