import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ConformpasswordComponent } from './conformpassword/conformpassword.component';
import { UsersComponent } from './users.component';

const routes: Routes = [{ path: '', component: UsersComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
