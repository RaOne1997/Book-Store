import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateRefundResponce, GetPaymentDetails, Instamojo, Refund } from '../../instamojo-api/models';
import type { Paymentreturn } from '../payment/models';

@Injectable({
  providedIn: 'root',
})
export class Payment_ServicesService {
  apiName = 'Default';
  

  callback = (payment: string) =>
    this.restService.request<any, GetPaymentDetails>({
      method: 'POST',
      url: '/api/app/payment_Services/callback',
      params: { payment },
    },
    { apiName: this.apiName });
  

  createPaymentOrderByObjClassAndID = (objClass: Instamojo, ID: string) =>
    this.restService.request<any, Paymentreturn>({
      method: 'POST',
      url: '/api/app/payment_Services/payment-order',
      params: { id: ID },
      body: objClass,
    },
    { apiName: this.apiName });
  

  refundByPayment = (payment: Refund) =>
    this.restService.request<any, CreateRefundResponce>({
      method: 'POST',
      url: '/api/app/payment_Services/refund',
      body: payment,
    },
    { apiName: this.apiName });
  

  start = (id: string) =>
    this.restService.request<any, Paymentreturn>({
      method: 'POST',
      url: `/api/app/payment_Services/${id}/start`,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
