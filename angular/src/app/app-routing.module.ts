import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClassbackComponent } from './My component/classback/classback.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  { path: 'books', loadChildren: () => import('./My component/book/book.module').then(m => m.BookModule) },

  { path: 'books/callback', component:ClassbackComponent },
  { path: 'email', loadChildren: () => import('./My component/email/email.module').then(m => m.EmailModule) },
  

  { path: 'Razerpay', loadChildren: () => import('./My component/raza-pay/Payment-modul').then(m => m.PaymentModule) },
  { path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule) },
  { path: 'boocking', loadChildren: () => import('./boocking/boocking.module').then(m => m.BoockingModule) },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
