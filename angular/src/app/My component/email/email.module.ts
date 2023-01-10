
import { CommonModule } from '@angular/common';

import { EmailRoutingModule } from './email-routing.module';
import { EmailComponent } from './email.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { EmailTemplateComponent } from './email-template/email-template.component';
import { NgModule } from '@angular/core';
import {EditorModule} from 'primeng/editor';


@NgModule({
  declarations: [
    EmailComponent,
    EmailTemplateComponent
  ],
  imports: [
    CommonModule,
    EmailRoutingModule,SharedModule,EditorModule
  
    
    
  ]
})
export class EmailModule { }
