import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { RazerpayservicesService } from '@proxy/bookstore/razerpay';
import { RegistrationModel } from '@proxy/bulkey-book/models/razorepay';

import { ScriptService } from './ScriptService';
declare let Razorpay: any;
@Component({
  selector: 'app-raza-pay',
  templateUrl: './raza-pay.component.html',
  styleUrls: ['./raza-pay.component.scss']
})


export class RazaPayComponent implements OnInit {
  mode: RegistrationModel
  razorpayOptions = {
    "key": "",
    "currency": "",
    "name": "",
    "description": "",
    "image": "",
    "order_id": "",

    "handler": (response) => {
      console.log(response)
    }
  }
  returnrespe: returnresponce = new returnresponce();
  constructor(private razerpay: RazerpayservicesService, private RZPS: ScriptService, private route: Router) {

    RZPS.load('Razorpay')
    // RZPS.loadCSS('MDB')


  }


  options = {
    "key": "rzp_test_nhrFMzGsypzvM5", // Enter the Key ID generated from the Dashboard
    "amount": "50000", // Amount is in currency subunits. Default currency is INR. Hence, 50000 refers to 50000 paise
    "currency": "INR",
    "name": "Acme Corp",
    "description": "Test Transaction",
    "image": "https://example.com/your_logo",
    "order_id": "", //This is a sample Order ID. Pass the `id` obtained in the response of Step 1
    "callback_url": "http://localhost:4200/books/callback/",

    "prefill": {
      "name": "Gaurav Kumar",
      "email": "gaurav.kumar@example.com",
      "contact": "9999999999"
    },
    "notes": {
      "address": "Razorpay Corporate Office"
    },
    "theme": {
      "color": "#3399cc"
    }
  };

  ngOnInit(): void {
  }
  byerezurepay() {

    // this.razorpayOptions=formdata

    this.mode = {
      name: "Abhijeet",
      mobile: "Abhijeet",
      email: "Abhijeet",
      amount: 500,
      notes:{"":""}

    }
    this.razerpay.getPaymentByRegistration(this.mode).subscribe({
      next: (rec) => {

        console.log(rec)
        this.razorpayOptions.key = rec.key
        this.razorpayOptions.currency = rec.currency
        this.razorpayOptions.name = rec.name
        this.razorpayOptions.description = rec.description
        this.razorpayOptions.image = rec.imageLogUrl
        this.razorpayOptions.order_id = rec.orderId
        this.razorpayOptions.handler = this.razorpayResponcehandler
        var rzp1 = new Razorpay(this.razorpayOptions)
        rzp1.open();
        console.log("Opening")
      }
    })

  }
  razorpayResponcehandler(responce) {
console.log(responce)
    this.returnrespe = responce
    this.returnrespe.paymentstatus
    window.location.replace("http://localhost:4200/books/callback")
    // console.log(responce)

  }

  rzp1
  clicktopay() {
    this.rzp1 = new Razorpay(this.options)
    console.log(this.rzp1)
    this.rzp1.open()

    this.rzp1.on('payment.failed', function (response) {
      console.log(response.error);
    })
  }


  redirecttoall(){
    this.route.navigate(['display']);

  }
}





export class returnresponce {
  razorpay_order_id: string
  razorpay_payment_id: string
  razorpay_signature: string
  paymentstatus: boolean = false

}