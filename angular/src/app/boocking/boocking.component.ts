import { Component, ElementRef, OnDestroy, OnInit } from '@angular/core';
import {MenuItem, MessageService} from 'primeng/api';
import { Subscription } from 'rxjs';
import { TicketService } from './ticketservice';
@Component({
  selector: 'app-boocking',
  templateUrl: './boocking.component.html',
  styleUrls: ['./boocking.component.scss'],
  providers:[MessageService,TicketService]
})
export class BoockingComponent implements OnInit,OnDestroy {
  items: MenuItem[];
  vac:any
  constructor(public messageService: MessageService, public ticketService: TicketService,private elRef: ElementRef) { }
  subscription: Subscription;
  ngOnInit(): void {
    //this.items = [{
//       label: 'Personal',
//       routerLink: 'personal'
//   },
//   {
//       label: 'Seat',
//       routerLink: 'seat'
//   },
//   {
//       label: 'Payment',
//       routerLink: 'payment'
//   },
//   {
//       label: 'Confirmation',
//       routerLink: 'confirmation'
//   }
// ];
// this.subscription = this.ticketService.paymentComplete$.subscribe((personalInformation) =>{
//   this.messageService.add({severity:'success', summary:'Order submitted', detail: 'Dear, ' + personalInformation.firstname + ' ' + personalInformation.lastname + ' your order completed.'});
// });
  }
ngOnDestroy(): void {
  if (this.subscription) {
    this.subscription.unsubscribe();
}
  
}

upload(s) {
  const file = s?.target?.files[0];

  // const preview = document.getElementById('preview');
  var div = this.elRef.nativeElement.querySelector('#viwesss')
  const reader = new FileReader();
  let byteArray;

  reader.onloadend = (e) => {
    this.vac = reader.result;
    if (this.vac != null) {
      var abcs = this.vac.indexOf(',')
      // this.getuserforedit.extraProperties.Profilepic = this.vac.substring(abcs + 1)
div.src =this.vac
     
    }

  };


  if (file) {
    reader.readAsDataURL(file);
  }
}
}
