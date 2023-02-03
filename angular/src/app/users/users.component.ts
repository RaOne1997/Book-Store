import { ExtensibleObject, ListService, PagedResultDto } from '@abp/ng.core';
import { GetIdentityUsersInput, IdentityUserDto, IdentityUserService, IdentityUserUpdateDto, UserData } from '@abp/ng.identity/proxy';
import { CurrentUserComponent } from '@abp/ng.theme.basic';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FileService } from '@proxy/blo-bstorage';
import { GetBlobRequestDto } from '@proxy/blob-storage';
import { CostumeIDenitytService } from '@proxy/identity';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],

  providers: [ListService],

})
export class UsersComponent implements OnInit {

  Users: PagedResultDto<IdentityUserDto> = new PagedResultDto<IdentityUserDto>();

  page: number = 1;
  count: number = 0;
  tableSize: number = 7;
  tableSizes: any = [3, 6, 9, 12];

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

  filter: GetIdentityUsersInput = {
    skipCount: 0,
    filter: null,
    maxResultCount: 5

  };
  constructor(private IdentityUser: CostumeIDenitytService, private confirmation: ConfirmationService,
    public readonly list: ListService,
    private blobstorage: FileService
  ) {


  }
  save() {
    this.isModalOpen = true
    console.log(this.isModalOpen)


  }

  ngAfterViewInit(): void {
    // //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    // //Add 'implements AfterViewInit' to the class.


  }



  displaypdf: any

  ngOnInit(): void {
    this.getdata(this.filter)
    // var  abc ={}as GetBlobRequestDto
    // abc.name="PDF"
    // this.blobstorage.getBlob(abc).forEach(x=>this.displaypdf=x.content)
    // const bookStreamCreator = (query) => this.IdentityUser.getList(query);

    // this.list.hookToQuery(bookStreamCreator).subscribe((response) => {
    //   this.Users = response;
    //   console.log( this.Users)

    // });



    // this.abc.extraProperties.Gender = 'm'
    // console.log(this.abc)
    // this.IdentityUser.getList(this.filter).subscribe(rec => { this.Users = rec })
    // this.IdentityUser.update(,).subscribe(rec => { this.Users = rec })

  }
  resultrange(e) {
    switch (e) {
      case 1:
        return this.filter.maxResultCount * this.page > this.Users.totalCount ? this.Users.totalCount : this.filter.maxResultCount * this.page
      case 2:
        return (this.filter.maxResultCount * this.page) - 4

    }
  }
  getdata(filter: GetIdentityUsersInput) {

    this.IdentityUser.getList(filter).subscribe((resp) => {

      this.Users = resp
      console.log(this.Users)
    });

    // this.list.hookToQuery(bookStreamCreator).subscribe((response) => {
    //   this.Users = response;
    //   console.log( this.Users)
  }

  onTableSizeChange(event: any): void {
    this.tableSize = event.target.value;
    this.page = 1;

    this.getdata(this.filter)
  }
  onTableDataChange(event) {
    console.log(event)
    this.page = event;
    this.filter.skipCount = this.filter.maxResultCount * (this.page - 1)
    this.getdata(this.filter)
    // this.fetchPosts();
  }
  addItem() {
    this.getdata(this.filter)


  }
  deleteuser(usedid) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        // this.IdentityUser.delete(usedid).subscribe(() => this.list.get());
      }
    });
  }
}

export interface Options {
  x?: string;
  y?: number;
}