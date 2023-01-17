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
import { first } from 'rxjs';
import { AutoMapper } from 'src/app/Automapper/AutoMapper';

@Component({
  selector: 'app-imagetest',
  templateUrl: './imagetest.component.html',
  styleUrls: ['./imagetest.component.scss']
})
export class ImagetestComponent extends AutoMapper implements OnInit {
  @Output() newItemEvent = new EventEmitter();
  possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrst1234567890*&^%$#@!~";
  lengthOfCode = 40;
  rolse: ListResultDto<IdentityRoleDto> = new ListResultDto<IdentityRoleDto>
  password: string;
  confrmpassword: string
  getuserforedit = {} as IdentityUserDto;
  abc = {} as IdentityUserUpdateDto;
  imagesave = {} as SaveBlobInputDto
  randompassword = true
  dropdownList: role[] = [];
  createuser = {} as IdentityUserCreateDto
  isModalOpen = false
  UserID: string
  title = titleTypeOptions
  data: role[] = []
  vac: any
  Da: Date = new Date()
  constructor(private IdentityUser: costumeIDenitytService,
    private confirmation: ConfirmationService,
    private identityrole: IdentityRoleService,
    private blobstorage: FileService) {
    super();
  }
  ngOnInit(): void {
    this.getrolesfordropdown();
  }
  addNewItem() {
    this.newItemEvent.emit();
  }
  binding: string[] = []
  savedata() {
    if (this.UserID == null)
      this.create();
    else
      this.update()
  }


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
    var User = this.ObjectMap<IdentityUserDto, IdentityUserCreateDto>(this.getuserforedit, this.createuser);
    User.password = this.password
        this.IdentityUser.create(User).subscribe(rec => {
      this.isModalOpen = false, this.confirmation.success("save successfully", 'record saved').forEach(x => { x }),
        this.addNewItem()
      this.clearfild()
    })
  }
  clearfild() {
    this.getuserforedit={} as IdentityUserDto;
    this.binding = [];
    this.UserID = null;
  }

  update() {
     var User = this.ObjectMap<IdentityUserDto, IdentityUserUpdateDto>(this.getuserforedit, this.abc);
    console.log(User)
     this.IdentityUser.update(this.UserID, User).subscribe(rec => { this.isModalOpen = false, this.addNewItem(), this.clearfild() })
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





