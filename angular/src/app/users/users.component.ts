import { ExtensibleObject, ListService, PagedResultDto } from '@abp/ng.core';
import { GetIdentityUsersInput, IdentityUserDto, IdentityUserService, IdentityUserUpdateDto } from '@abp/ng.identity/proxy';
import { AfterViewInit, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  providers: [ListService]
})
export class UsersComponent implements OnInit {
  isModalOpen = false
  abc: IdentityUserUpdateDto = {
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
    extraProperties: {}


  }

  Users: PagedResultDto<IdentityUserDto> = new PagedResultDto<IdentityUserDto>()
  filter: GetIdentityUsersInput = {
    skipCount: 0,
    filter: null,
    maxResultCount: 200

  };
  constructor(private IdentityUser: IdentityUserService, public readonly list: ListService,) {


  }
  save() {
    this.isModalOpen = true
    console.log(this.isModalOpen)


  }

  ngAfterViewInit(): void {
    // //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    // //Add 'implements AfterViewInit' to the class.


  }
  ngOnInit(): void {
    const bookStreamCreator = (query) => this.IdentityUser.getList(query);

    this.list.hookToQuery(bookStreamCreator).subscribe((response) => {
      this.Users = response;
    });

    // this.abc.extraProperties.Gender = 'm'
    // console.log(this.abc)
    // this.IdentityUser.getList(this.filter).subscribe(rec => { this.Users = rec })
    // this.IdentityUser.update(,).subscribe(rec => { this.Users = rec })

  }
  addItem() {
   this.list.get()
  }
  edituser(userid) {
    this.IdentityUser.get(userid).subscribe(rec => { console.log(rec) })

  }
}

export interface Options {
  x?: string;
  y?: number;
}