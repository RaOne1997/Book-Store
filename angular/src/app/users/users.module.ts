import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UsersComponent } from './users.component';
import { SharedModule } from '../shared/shared.module';
import { ImagetestComponent } from './imagetest/imagetest.component';
import {MultiSelectModule} from 'primeng/multiselect';

import {AccordionModule} from 'primeng/accordion';
import {ButtonModule} from 'primeng/button';     //accordion and accordion tab
import { NgxPaginationModule } from 'ngx-pagination';
import { ConformpasswordComponent } from './conformpassword/conformpassword.component';

@NgModule({
  declarations: [
    UsersComponent,
    ImagetestComponent,
   
    ConformpasswordComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    UsersRoutingModule,
    MultiSelectModule,
    NgxPaginationModule

    
    
  ]
})
export class UsersModule { }
