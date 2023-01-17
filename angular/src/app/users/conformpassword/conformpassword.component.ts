import { ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { TestService } from '@proxy/identity';
import { ResetPasswordDto } from '@proxy/volo/abp/account';

@Component({
  selector: 'app-conformpassword',
  templateUrl: './conformpassword.component.html',
  styleUrls: ['./conformpassword.component.scss']
})
export class ConformpasswordComponent implements OnInit {
  password1; 
  password2;
  parms:any
  input ={}as ResetPasswordDto;
  constructor(private rot:ActivatedRoute,private TestServices :TestService,private confirmation: ConfirmationService,) { }

  ngOnInit(): void {
    this.rot.params.forEach(x=> {this.parms=x ;console.log(this.parms)})
  }

  reset()
  {
    this.input.password = this.password1
    this.input.resetToken = this.parms["resetToken"]
    this.input.userId = this.parms["userId"]
    console.log(this.input)
    this.TestServices.resetPasswordtest(this.input).subscribe((rec)=>{
      this.confirmation.success("Password changes successfuly","Reset Password")
    })
  }
}
