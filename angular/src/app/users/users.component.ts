import { ExtensibleObject, ListService, PagedResultDto } from '@abp/ng.core';
import { GetIdentityUsersInput, IdentityUserDto, IdentityUserService, IdentityUserUpdateDto } from '@abp/ng.identity/proxy';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
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
  constructor(private IdentityUser: IdentityUserService, private confirmation: ConfirmationService,
    public readonly list: ListService,) {


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
  deleteuser(usedid) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.IdentityUser.delete(usedid).subscribe(() => this.list.get());
      }
    });
  }
}

export interface Options {
  x?: string;
  y?: number;
}