import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Payment_ServicesService } from '@proxy/bookstore/payment';
import { GetPaymentDetails } from '@proxy/instamojo-api';



@Component({
  selector: 'app-paymentstatus',
  templateUrl: './paymentstatus.component.html',
  styleUrls: ['./paymentstatus.component.scss']
})
export class PaymentstatusComponent implements OnInit {

  constructor(private rout: ActivatedRoute, private payment: Payment_ServicesService,) { }

  paramateris:qprams={
    id:"",
    payment_id:"",
    payment_status:"",
    transaction_id:"",

  };
  paymentstatus: GetPaymentDetails;


  ngOnInit(): void {
    this.rout.queryParams.subscribe({
      next: (params) => {
        this.paramateris.id = params.id
        this.paramateris.payment_id = params.payment_id
        this.paramateris.payment_status = params.payment_status
        this.paramateris.transaction_id = params.transaction_id
        console.log(this.paramateris)
      }
    });

     this.payment.callback(this.paramateris.payment_id).subscribe({ next: (status) => { this.paymentstatus=status 
    
    console.log(this.paymentstatus)
    
    } })

  }

}


export class qprams{
  id !:string
  payment_id !:string
  payment_status !:string
  transaction_id !:string

};