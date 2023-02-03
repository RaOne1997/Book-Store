import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BoockingRoutingModule } from './boocking-routing.module';
import { BoockingComponent } from './boocking.component';
import {StepsModule} from 'primeng/steps';
import {ToastModule} from 'primeng/toast';
import { PersonInformationComponent } from './person-information/person-information.component';
import { SeatComponent } from './seat/seat.component';
import { PaymentComponent } from './payment/payment.component';
import {CardModule} from 'primeng/card';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { SharedModule } from 'primeng/api';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
@NgModule({
  declarations: [
    BoockingComponent,
    PersonInformationComponent,
    SeatComponent,
    PaymentComponent,
    ConfirmationComponent,
    
  ],
  imports: [
   
    CommonModule,
    SharedModule,FormsModule,
    NgxPaginationModule,
    BoockingRoutingModule,
    StepsModule,
    ToastModule,
    CardModule
    
  ]
})
export class BoockingModule { }
