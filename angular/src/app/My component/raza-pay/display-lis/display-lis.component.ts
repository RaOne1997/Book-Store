import { Component, OnInit } from '@angular/core';
import { RazerpayservicesService } from '@proxy/bookstore/razerpay';
import { Paymentdetails } from '@proxy/bulkey-book/models/razorepay';

import { RazerpayservicesApi } from '../ApiServices/ApiservicesRazerpay';
import { ScriptService } from '../ScriptService';

@Component({
  selector: 'app-display-lis',
  templateUrl: './display-lis.component.html',
  styleUrls: ['./display-lis.component.scss']
})
export class DisplayLisComponent implements OnInit {

  _paymentdetails: Paymentdetails={
    count: 0,
    items: []
    
  };
  constructor(private razerpay: RazerpayservicesService,private RZPS: ScriptService) {
    // RZPS.loadCSS('MDB')

  }
  ngOnInit(): void {
    this.razerpay.allPayment().forEach(x =>this._paymentdetails = x)

  } 


  getKeys(obj: any) {

    return Object.keys(obj)

  }
}

export class paymentdetails {
  entity: string
  count: number
  items: items
}

export class items {
  id: string
  entity: string
  amount: number
  currency: string
  status: string
  order_id: string
  invoice_id: string
  international: string
  method: string
  amount_refunded: number
  refund_status: string
  captured: true
  description: string
  card_id: any
  bank: any
  wallet: string
  vpa: any
  email: string
  notes: any
  contact: any
  fee: number
  tax: number
  error_code: any
  error_description: any
  error_source: any
  error_step: any
  error_reason: any
  transaction_id: any
  acquirer_data: any
  created_at: number
}