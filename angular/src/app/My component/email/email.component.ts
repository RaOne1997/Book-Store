import { Component, OnInit } from '@angular/core';
import { EmailService, EmailSettingsDTO } from '@proxy/emailsend';


@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.scss']
})
export class EmailComponent implements OnInit {
  emailcon: EmailSettingsDTO = {
    emailId: '',
    name: '',
    password: '',
    host: '',
    port: 0,
    useSSL: false,
  };
    isModalOpen = false;
  constructor(private emservices:EmailService) { }

  ngOnInit(): void {
  }


  saveconfigration() {

    this.emservices.create(this.emailcon).subscribe({next:(rec)=>{
      this.emailcon=rec
      console.log(this.emailcon)
    }})


  }

  
  TestSendEmail() {


    this.isModalOpen = true;
    
    
  }
  
  
  sendsms(){
    this.emservices.testSendEmail(this.emailcon,"abhijeet.warade@waiin.com").subscribe({next:(rec)=>{ console.log(rec)}})
    this.isModalOpen = true;

  }
}
