import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { Paymentdetails, RazorPayOptionsModel, RefundData, RefundReSopnce, RegistrationModel } from '../../bulkey-book/models/razorepay/models';

@Injectable({
  providedIn: 'root',
})
export class RazerpayservicesService {
  apiName = 'Default';
  

  allPayment = () =>
    this.restService.request<any, Paymentdetails>({
      method: 'POST',
      url: '/api/app/razerpayservices/all-payment',
    },
    { apiName: this.apiName });
  

  fullRefundByPaymentIdAndRefundData = (paymentId: string, refundData: RefundData) =>
    this.restService.request<any, RefundReSopnce>({
      method: 'POST',
      url: `/api/app/razerpayservices/full-refund/${paymentId}`,
      body: refundData,
    },
    { apiName: this.apiName });
  

  getPaymentByRegistration = (registration: RegistrationModel) =>
    this.restService.request<any, RazorPayOptionsModel>({
      method: 'GET',
      url: '/api/app/razerpayservices/payment',
      params: { name: registration.name, mobile: registration.mobile, email: registration.email, amount: registration.amount, notes: registration.notes },
    },
    { apiName: this.apiName });
  

  partialRefundByPaymentIdAndRefundData = (paymentId: string, refundData: RefundData) =>
    this.restService.request<any, RefundReSopnce>({
      method: 'POST',
      url: `/api/app/razerpayservices/partial-refund/${paymentId}`,
      body: refundData,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
