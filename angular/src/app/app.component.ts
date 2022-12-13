import { ReplaceableComponentsService } from '@abp/ng.core';
import { eIdentityComponents } from '@abp/ng.identity';
import { eThemeBasicComponents } from '@abp/ng.theme.basic';
import { NavItem } from '@abp/ng.theme.shared';
import { Component, TrackByFunction } from '@angular/core';
import { COstumnavbar } from './navbarcomponent/navbar-component';
import { NavbarwithsideComponent } from './navbarwithside/navbarwithside.component';
import { ImagetestComponent } from './users/imagetest/imagetest.component';
import { UsersComponent } from './users/users.component';
@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
  `,
})
export class AppComponent {

constructor( private replaceableComponents: ReplaceableComponentsService){
  this.replaceableComponents.add({
    component: NavbarwithsideComponent,
    key: eThemeBasicComponents.ApplicationLayout
  });

  this.replaceableComponents.add({
    component: ImagetestComponent,
    key: eIdentityComponents.Users
  });
}
}
  // trackByFn: TrackByFunction<NavItem> = (_, element) =>console.log( element.id);

