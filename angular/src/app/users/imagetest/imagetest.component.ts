import { ListResultDto } from '@abp/ng.core';
import { IdentityRoleDto, IdentityRoleService, IdentityUserCreateDto, IdentityUserDto, IdentityUserService, IdentityUserUpdateDto } from '@abp/ng.identity/proxy';
import { DOCUMENT } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';

import { costumeIDenitytService } from '@proxy/identity';

import { Inject } from '@angular/core';
import { ConfirmationService } from '@abp/ng.theme.shared';
import { titleTypeOptions } from './TitleType';
import { FileService } from '@proxy/blo-bstorage';
import { SaveBlobInputDto } from '@proxy/blob-storage';
import { DateTimeAdapter } from '@abp/ng.theme.shared/extensions';

@Component({
  selector: 'app-imagetest',
  templateUrl: './imagetest.component.html',
  styleUrls: ['./imagetest.component.scss']
})
export class ImagetestComponent implements OnInit, AfterViewInit {
  @Output() newItemEvent = new EventEmitter();
  @ViewChild('myDiv') myDiv: ElementRef


  ngOnDestroy(): void {
    console.log('distory')

  }
  possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrst1234567890*&^%$#@!~";
  lengthOfCode = 40;
  rolse: ListResultDto<IdentityRoleDto> = new ListResultDto<IdentityRoleDto>
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
    extraProperties: { Gender: '', Profilepic: null, Title: 0 },
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
  abc: IdentityUserUpdateDto = {
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
  imagesave = {} as SaveBlobInputDto
  randompassword = true
  dropdownList: role[] = [];
  createuser: IdentityUserCreateDto = {
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
  constructor(private IdentityUser: costumeIDenitytService,
    private confirmation: ConfirmationService,
    private identityrole: IdentityRoleService,
    private blobstorage: FileService) {



  }
  vac: any

    ;
    Da:Date=new Date()

  ngAfterViewInit(): void {


    //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    //Add 'implements AfterViewInit' to the class.

  }
  ngOnInit(): void {
    this.getrolesfordropdown();

    console.log("oninit")
  }
  addNewItem() {
    this.newItemEvent.emit();
  }
  binding: string[] = []
  savedata() {
    console.log(this.getuserforedit)

    if (this.UserID == null)
      this.create();
    else
      this.update()

  }
  data: role[] = []

  getrolesfordropdown() {
    this.identityrole.getAllList().subscribe(rec => {
      this.rolse = rec, this.rolse.items.forEach(x => {
        var abc = new role();
        abc.item_text = x.name
        abc.item_id = x.name
        this.dropdownList.push(abc)
      })
    })
  }

  upload(s) {
    const file = s?.target?.files[0];

    // const preview = document.getElementById('preview');
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
  create() {
    if (this.randompassword) {
      this.password = this.makeRandom(6, this.possible)
    }
    this.createuser.password = this.password
    this.createuser.userName = this.getuserforedit.userName,
      this.createuser.name = this.getuserforedit.name,
      this.createuser.surname = this.getuserforedit.surname,
      this.createuser.email = this.getuserforedit.email,
      this.createuser.phoneNumber = this.getuserforedit.phoneNumber,
      this.createuser.isActive = this.getuserforedit.isActive,
      this.createuser.lockoutEnabled = this.getuserforedit.lockoutEnabled,
      this.createuser.roleNames = this.binding
    this.createuser.extraProperties.Gender = this.getuserforedit.extraProperties.Gender,
      this.createuser.extraProperties.Profilepic = this.getuserforedit.extraProperties.Profilepic
    this.createuser.extraProperties.Title = this.getuserforedit.extraProperties.Title
    this.imagesave.name = this.getuserforedit.name
    this.imagesave.content = this.getuserforedit.extraProperties.Profilepic
    // this.blobstorage.saveBlob(this.imagesave).subscribe(rec=>{
    //   this.isModalOpen = false, this.confirmation.success("save successfully", 'record saved to blob storage').forEach(x => { x }),
    //   this.addNewItem()
    // })
    this.IdentityUser.create(this.createuser).subscribe(rec => {
      this.isModalOpen = false, this.confirmation.success("save successfully", 'record saved').forEach(x => { x }),
        this.addNewItem()



      this.clearfild()
    })

  }
  clearfild() {

    this.getuserforedit.userName = null,
      this.getuserforedit.name = null,
      this.getuserforedit.surname = null,
      this.getuserforedit.email = null,
      this.getuserforedit.phoneNumber = null,
      this.binding = []
    this.getuserforedit.extraProperties.Gender = null,
      this.getuserforedit.extraProperties.Profilepic = null
    this.getuserforedit.extraProperties.Title = 0
    this.UserID = null

  }


  update() {

    //this.abc.password = this.password
    this.abc.userName = this.getuserforedit.userName,
      this.abc.name = this.getuserforedit.name,
      this.abc.surname = this.getuserforedit.surname,
      this.abc.email = this.getuserforedit.email,
      this.abc.phoneNumber = this.getuserforedit.phoneNumber,
      this.abc.isActive = this.getuserforedit.isActive,
      this.abc.lockoutEnabled = this.getuserforedit.lockoutEnabled,
      this.abc.roleNames = this.binding,
      this.abc.extraProperties.Gender = this.getuserforedit.extraProperties.Gender,
      this.abc.extraProperties.Profilepic = this.getuserforedit.extraProperties.Profilepic
    this.abc.extraProperties.Title = this.getuserforedit.extraProperties.Title
    console.log(this.abc)
    this.IdentityUser.update(this.UserID, this.abc).subscribe(rec => { this.isModalOpen = false, this.addNewItem(), this.clearfild() })

  }
  show(UserID = null) {
    this.UserID = UserID
    if (UserID != null) {

      this.IdentityUser.get(UserID).forEach(element => {
        this.getuserforedit = element
        console.log(this.getuserforedit)

      });
      this.IdentityUser.getRoles(UserID).forEach(x => {
        x.items.forEach(
          roless => {
            var an = roless.name
            this.binding.push(an)
          }
        )
      })
    }
    this.dropdownList = []
    this.getrolesfordropdown();
    console.log(this.UserID)
    console.log(this.makeRandom(4, this.possible))


    this.isModalOpen = true
  }
  makeRandom(lengthOfCode: number, possible: string) {
    let text = "";
    for (let i = 0; i < lengthOfCode; i++) {
      text += possible.charAt(Math.floor(Math.random() * possible.length));
    }
    return text;
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

export class role {
  item_id: string;
  item_text: string
}
