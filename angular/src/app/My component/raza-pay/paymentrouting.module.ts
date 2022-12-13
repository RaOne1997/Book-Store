import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RazaPayComponent } from '../raza-pay/raza-pay.component';
import { DisplayLisComponent } from './display-lis/display-lis.component';


const routes: Routes = [{ path: '', component: RazaPayComponent, children: [
    {
      path: 'display', // child route path
      component: DisplayLisComponent, // child route component that the router renders
    },
  
  ],canActivate: [AuthGuard, PermissionGuard]},

// { path: 'display', component: DisplayLisComponent, canActivate: [AuthGuard, PermissionGuard]},



];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],

})
export class PaymentRoutingModule { }
