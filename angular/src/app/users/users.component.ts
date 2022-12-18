import { ExtensibleObject, PagedResultDto } from '@abp/ng.core';
import { GetIdentityUsersInput, IdentityUserDto, IdentityUserService, IdentityUserUpdateDto } from '@abp/ng.identity/proxy';
import { AfterViewInit, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  isModalOpen=false
   abc :IdentityUserUpdateDto ={
    password: '',
    concurrencyStamp: '',

    userName: '',
    name: '',
    surname: '',
    email: '',
    phoneNumber: '',
    isActive: false,
    lockoutEnabled: false,
    roleNames: [],
    extraProperties:{}
    

   }

  Users: PagedResultDto<IdentityUserDto> = new PagedResultDto<IdentityUserDto>()
  filter: GetIdentityUsersInput = {
    skipCount: 0,
    filter: null,
    maxResultCount: 200

  };
  constructor(private IdentityUser: IdentityUserService) {


  }
  save(){
    this.isModalOpen=true
    console.log(this.isModalOpen)


  }

  ngAfterViewInit(): void {
    // //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    // //Add 'implements AfterViewInit' to the class.


  }
  ngOnInit(): void {
    
this.abc.extraProperties.Gender='m'
console.log(this.abc)
    this.IdentityUser.getList(this.filter).subscribe(rec => { this.Users = rec })
    // this.IdentityUser.update(,).subscribe(rec => { this.Users = rec })

  }
  edituser(userid) {
    this.IdentityUser.get(userid).subscribe(rec => { console.log(rec) })

  }
}

export interface Options {
  x?: string;
  y?: number;
}