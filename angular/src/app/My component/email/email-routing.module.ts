import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmailTemplateComponent } from './email-template/email-template.component';
import { EmailComponent } from './email.component';

const routes: Routes = [{ path: '', component: EmailComponent },
{ path: 'email-templete', component: EmailTemplateComponent  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmailRoutingModule { }
