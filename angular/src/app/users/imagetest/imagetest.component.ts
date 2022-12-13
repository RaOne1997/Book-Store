import { IdentityUserCreateDto, IdentityUserService, IdentityUserUpdateDto } from '@abp/ng.identity/proxy';
import { Component, OnInit } from '@angular/core';
import { costumeIDenitytService } from '@proxy/identity';

@Component({
  selector: 'app-imagetest',
  templateUrl: './imagetest.component.html',
  styleUrls: ['./imagetest.component.scss']
})
export class ImagetestComponent implements OnInit {

  abc :IdentityUserCreateDto ={
    password: '',
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
  constructor(private IdentityUser: costumeIDenitytService) { }
vac:any
  ngOnInit(): void {
  }


  savedata(){
  
     var abcs= this.vac.indexOf(',')
    this.abc.password='Abhi@123'
  
      this.abc.userName= 'abhisas',
      this.abc.name= 'abhi',
      this.abc.surname= 'warade',
      this.abc.email= 'varadass@gmail.com',
      this.abc.phoneNumber= '7057445611',
      this.abc.isActive= true,
      this.abc.lockoutEnabled= true,
      this.abc.roleNames= ["admin"],
      this.abc.extraProperties.Gender='M',
      this.abc.extraProperties.Profilepic=this.vac.substring(abcs+1)
       this.abc.extraProperties.Title= 0
      console.log(this.abc)
      this.IdentityUser.create(this.abc).subscribe(rec=>{console.log(rec)})


  }
   upload(s) {
    const file = s?.target?.files[0];
    
    const preview = document.getElementById('preview'); 
    const reader = new FileReader();
    let byteArray;

    reader.onloadend = (e) => {
      // console.log(reader.result);
      this.vac = reader.result ;
    
   };


    if (file) {
      reader.readAsDataURL(file);
    }
  }

}
function convertDataURIToBinary(result ): any {
  var base64Index = result.indexOf(';base64,') + ';base64,'.length;
    var base64 = result.substring(base64Index);
    console.log(base64)
    var raw = window.atob(base64);
    var rawLength = raw.length;
    var array = new Uint8Array(new ArrayBuffer(rawLength));
  
    for(let i = 0; i < rawLength; i++) {
      array[i] = raw.charCodeAt(i);
    }
    return array;
}

