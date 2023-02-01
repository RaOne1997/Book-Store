import type { CreateOrderInput, OrderDTO } from './models';
import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { OrderName } from '../order-modul/models';

@Injectable({
  providedIn: 'root',
})
export class OrderAppServicesService {
  apiName = 'Default';
  

  create = (input: CreateOrderInput) =>
    this.restService.request<any, OrderDTO>({
      method: 'POST',
      url: '/api/app/order-app-services/asyncaaa',
      body: input,
    },
    { apiName: this.apiName });
  

  getOrderByCustomerIdByInput = (input: string) =>
    this.restService.request<any, OrderName>({
      method: 'GET',
      url: '/api/app/order-app-services/order-by-customer-id',
      params: { input },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
