
import { BookRoutingModule } from './book-routing.module';
import { BookComponent } from './book.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { PaymentstatusComponent } from './paymentstatus/paymentstatus.component';

import { CreateinvoiceComponent } from '../createinvoice/createinvoice.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgModule } from '@angular/core';
import { APP_ROUTE_PROVIDER } from 'src/app/route.provider';
import { NgxPaginationModule } from 'ngx-pagination';


@NgModule({
  declarations: [BookComponent, PaymentstatusComponent,CreateinvoiceComponent],
  imports: [
    BookRoutingModule,
    SharedModule,
    NgbDatepickerModule,
    

  ],
  providers: [APP_ROUTE_PROVIDER],
})
export class BookModule { }

