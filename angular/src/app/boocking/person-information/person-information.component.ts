import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TicketService } from '../ticketservice';

@Component({
  selector: 'app-person-information',
  templateUrl: './person-information.component.html',
  styleUrls: ['./person-information.component.scss']
})
export class PersonInformationComponent implements OnInit {
  personalInformation: any;

    submitted: boolean = false;

    constructor(public ticketService: TicketService, private router: Router) { }

    ngOnInit() { 
        this.personalInformation = this.ticketService.getTicketInformation().personalInformation;
    }

    nextPage() {
        if (this.personalInformation.firstname && this.personalInformation.lastname && this.personalInformation.age) {
            this.ticketService.ticketInformation.personalInformation = this.personalInformation;
            this.router.navigate(['boocking/seat']);
console.log("inside if")
            return;
        }
        console.log("ddd")
        this.submitted = true;
    }

}
