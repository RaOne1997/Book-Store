import { ReplaceableComponentsService } from '@abp/ng.core';
import { eLayoutType } from '@abp/ng.core/public-api';
import { eIdentityComponents } from '@abp/ng.identity';
import { eThemeBasicComponents, eUserMenuItems } from '@abp/ng.theme.basic';
import { NavItem } from '@abp/ng.theme.shared';
import { Component, OnInit, TrackByFunction } from '@angular/core';
import { COstumnavbar } from './navbarcomponent/navbar-component';
import { NavbarwithsideComponent } from './navbarwithside/navbarwithside.component';
import { UsersComponent } from './users/users.component';
import { PrimeNGConfig } from 'primeng/api';
@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
  `,
})
export class AppComponent implements OnInit {

constructor( private replaceableComponents: ReplaceableComponentsService,private primengConfig: PrimeNGConfig){
  this.replaceableComponents.add({
    component: NavbarwithsideComponent,
    key: eThemeBasicComponents.ApplicationLayout
  });

  this.replaceableComponents.add({
    component: UsersComponent,
    key: eIdentityComponents.Users
  });


}
ngOnInit() {
  this.primengConfig.ripple = true;
}
}
  // trackByFn: TrackByFunction<NavItem> = (_, element) =>console.log( element.id);

