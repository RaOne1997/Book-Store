import { ConfirmationComponent } from '@abp/ng.theme.shared';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BoockingComponent } from './boocking.component';
import { PaymentComponent } from './payment/payment.component';
import { PersonInformationComponent } from './person-information/person-information.component';
import { SeatComponent } from './seat/seat.component';

const routes: Routes = [{ path: '', component: BoockingComponent ,children: [
  { path: '', redirectTo: 'personal', pathMatch: 'full' },
  { path: 'personal', component: PersonInformationComponent },
  { path: 'confirmation', component: ConfirmationComponent },
  { path: 'seat', component: SeatComponent },
  { path: 'payment', component: PaymentComponent }
]}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BoockingRoutingModule { }
