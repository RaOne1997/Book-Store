import {eLayoutType, SubscriptionService} from '@abp/ng.core';
import { collapseWithMargin, slideFromBottom } from '@abp/ng.theme.shared';
import {AfterViewInit, Component} from '@angular/core';
import { LayoutService } from '../../lib/services/layout.service';


@Component({
  selector: 'abp-layout-application',
  templateUrl: './navbar-component.html',
 
  animations: [slideFromBottom, collapseWithMargin],
  providers: [LayoutService, SubscriptionService],
})


export class COstumnavbar implements AfterViewInit {
    // required for dynamic component
    static type = eLayoutType.application;
  
    constructor(public service: LayoutService) {}
  
    ngAfterViewInit() {
      this.service.subscribeWindowSize();
    }
  }