import { IdentityUserCreateDto, IdentityUserDto, IdentityUserService, IdentityUserUpdateDto } from '@abp/ng.identity/proxy';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { TitleType, titleTypeOptions } from '@proxy/books';
import { costumeIDenitytService } from '@proxy/identity';


@Component({
  selector: 'app-imagetest',
  templateUrl: './imagetest.component.html',
  styleUrls: ['./imagetest.component.scss']
})
export class ImagetestComponent implements OnInit, OnDestroy {

  ngOnDestroy(): void {
    console.log('distory')

  }
  password: string;
  confrmpassword: string
  getuserforedit: IdentityUserDto = {
    concurrencyStamp: "",
    creationTime: "",
    creatorId: null,
    deleterId: null,
    deletionTime: null,
    email: "",
    emailConfirmed: false,
    extraProperties: {},
    id: "",
    isActive: true,
    isDeleted: false,
    lastModificationTime: "",
    lastModifierId: null,
    lockoutEnabled: true,
    lockoutEnd: null,
    name: "",
    phoneNumber: null,
    phoneNumberConfirmed: false,
    surname: null,
    tenantId: null,
    userName: ""
  }
  abc: IdentityUserCreateDto = {
    password: '',
    userName: '',
    name: '',
    surname: '',
    email: '',
    phoneNumber: '',
    isActive: false,
    lockoutEnabled: false,
    roleNames: [],
    extraProperties: { Gender: '', Title: 0, Profilepic: '' }


  }
  isModalOpen = false
  UserID: string
  title = titleTypeOptions
  constructor(private IdentityUser: costumeIDenitytService) { }
  vac: any
  randompassword = true
  ngOnInit(): void {
  }


  savedata() {

    var abcs = this.vac.indexOf(',')
    this.abc.password = 'Abhi@123'

    this.abc.userName = 'abhisas',
      this.abc.name = 'abhi',
      this.abc.surname = 'warade',
      this.abc.email = 'varadass@gmail.com',
      this.abc.phoneNumber = '7057445611',
      this.abc.isActive = true,
      this.abc.lockoutEnabled = true,
      this.abc.roleNames = ["admin"],
      this.abc.extraProperties.Gender = 'M',
      this.abc.extraProperties.Profilepic = this.vac.substring(abcs + 1)
    this.abc.extraProperties.Title = 0
    console.log(this.abc)
    this.IdentityUser.create(this.abc).subscribe(rec => { console.log(rec) })


  }
  upload(s) {
    const file = s?.target?.files[0];

    const preview = document.getElementById('preview');
    const reader = new FileReader();
    let byteArray;

    reader.onloadend = (e) => {
      this.vac = reader.result;
      if (this.vac != null) {
        var abcs = this.vac.indexOf(',')
        this.getuserforedit.extraProperties.Profilepic = this.vac.substring(abcs + 1)
      }

    };


    if (file) {
      reader.readAsDataURL(file);
    }
  }

  show(UserID) {
    if (UserID != null) {
      this.UserID = UserID
      this.IdentityUser.get(UserID).forEach(element => {
        this.getuserforedit = element
        console.log(this.getuserforedit)

      });
    }


    this.isModalOpen = true
  }

}
function convertDataURIToBinary(result): any {
  var base64Index = result.indexOf(';base64,') + ';base64,'.length;
  var base64 = result.substring(base64Index);
  console.log(base64)
  var raw = window.atob(base64);
  var rawLength = raw.length;
  var array = new Uint8Array(new ArrayBuffer(rawLength));

  for (let i = 0; i < rawLength; i++) {
    array[i] = raw.charCodeAt(i);
  }
  return array;
}

