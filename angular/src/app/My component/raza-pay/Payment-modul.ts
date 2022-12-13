import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import {  RouterModule} from '@angular/router';

import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';

import { SharedModule } from 'src/app/shared/shared.module';
import { DisplayLisComponent } from './display-lis/display-lis.component';
import { PaymentRoutingModule } from './paymentrouting.module';
import { RazaPayComponent } from './raza-pay.component';

@NgModule({
  declarations: [
    DisplayLisComponent,
    RazaPayComponent
  ],
  imports: [
    CommonModule ,
    PaymentRoutingModule,
    SharedModule,
    RouterModule,
    NgbDatepickerModule
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
})
export class PaymentModule { }

