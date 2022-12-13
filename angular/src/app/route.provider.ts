import { RoutesService, eLayoutType } from '@abp/ng.core';
import { eThemeSharedRouteNames } from '@abp/ng.theme.shared';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      
      {
        path: '/book-store',
        name: '::Menu:BookStore',
        iconClass: 'fas fa-book',
        order: 2,
        layout: eLayoutType.application,
      }, 
      {
        path: '/books',
        name: '::Menu:Books',
        parentName: '::Menu:BookStore',
        order: 1,

        layout: eLayoutType.application,
        // requiredPolicy: 'BookStore.Books',
      },

   
      {
        path: '/emailsend',
        name: '::Menu:Email',
        iconClass: 'fas fa-book',
        parentName: eThemeSharedRouteNames.Administration,
        order: 3,
        layout: eLayoutType.application,
      },
      {
        path: '/email',
        name: '::Menu:EmailConfigration',
        parentName: '::Menu:Email',
        layout: eLayoutType.application,
        // requiredPolicy: 'BookStore.Books',
      },

      {
        path: '/email/email-templete',
        name: '::Menu:EmailTemplate',
        parentName: '::Menu:Email',
        layout: eLayoutType.application,
        // requiredPolicy: 'BookStore.Books',
      },
      
       {
        path: '/Razerpay-payment',
        name: '::Menu:RazerPay',
        iconClass: 'fas fa-book',
        order: 4,
        layout: eLayoutType.application,
      },
      {
        path: '/Razerpay',
        name: 'Razerpay Getall',
        parentName: '::Menu:RazerPay',
        layout: eLayoutType.application,
        // requiredPolicy: 'BookStore.Books',
      },
    ]);
  };
}
