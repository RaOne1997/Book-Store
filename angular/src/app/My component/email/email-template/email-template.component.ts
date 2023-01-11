import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { TemplateType, templateTypeOptions } from '@proxy/books';
import { EmailData, EmailService, EmailtemplateDTO, Templatename } from '@proxy/emailsend';
declare var $: any;
@Component({
  selector: 'app-email-template',
  templateUrl: './email-template.component.html',
  styleUrls: ['./email-template.component.scss']
})
export class EmailTemplateComponent implements OnInit, AfterViewInit {
  tinyMceConfig: any
  binding: any
  template: Templatename[]
  test1: any
  isModalOpen = false;
  templatetype = templateTypeOptions;
  Tempdata: any
  savedata={} as EmailtemplateDTO 
  form: FormGroup;
  emaildata: EmailData
  text: string;
  ngAfterViewInit(): void {
  
   


  }
  constructor(private emservices: EmailService, private http: HttpClient,private sanitizer:DomSanitizer,
    private fb: FormBuilder,) {

  }

  createBook() {
    // add this line
    this.isModalOpen = true;
  }
 
  

  // handleFileInput(e: any) {
  //   this.Tempdata = e?.target?.files[0];
  // }
  ngOnInit(): void {
    // inits the collapsibel menus
    this.emservices.getAlltemplatename().subscribe(res => { this.template = res })
  }

  test = "hello"
  configureTinyMce() {
    this.tinyMceConfig = {
      selector: '#mytextarea',
      branding: false,
      /**
       * This is needed to prevent console errors
       * if you're hosting your own TinyMCE
       */
      // content_css: 'assets/tinymce/skins/ui/oxide/content.min.css',
      height: 400,
      image_advtab: true,
      imagetools_toolbar: `
        rotateleft rotateright |
        flipv fliph | 
        editimage imageoptions`,
      importcss_append: !0,
      inline: false,
      menubar: true,
      paste_data_images: !0,
      /**
       * This is needed to prevent console errors 
       * if you're hosting your own TinyMCE
       */
      // skin_url: 'assets/tinymce/skins/ui/oxide',
      toolbar: `
        insertText |
        copy undo redo formatselect |
        bold italic strikethrough forecolor backcolor |
        link | alignleft aligncenter alignright alignjustify |
        numlist bullist outdent indent |
        removeformat`,
      setup: (editor) => {
        editor.ui.registry.addButton('insertText', {
          text: 'Press Me To Insert Text!',
          onAction: () => {
            editor.insertContent('<strong>Hello World!</strong>');
          }
        });
      }
    };
  }


  sendemail(emailbody) {

    this.emaildata.emailBody = emailbody;
    this.emaildata.ishtmlTemplet = true;
    this.emaildata.emailSubject = "forgot pass"
    this.emaildata.emailToId = "varadeabhijeet@gmail.com"
    this.emservices.sendEmail(this.emaildata).subscribe(res => { console.log(res) })


  }
  save() {
    // if (this.form.invalid) {
    //   return;
    // }
    this.emservices.uploadFile(this.savedata).subscribe(rec=>{
    console.log(rec)
    })



console.log(this.savedata)
   
    this.isModalOpen = false;
    // this.form.reset();
    // this.list.get();

  }


vac:any;
data:any;
  handleFileInput(s) {
    const file = s?.target?.files[0];

    // const preview = document.getElementById('preview');
    const reader = new FileReader();
    let byteArray;

    reader.onloadend = (e) => {
      this.vac = reader.result;
      if (this.vac != null) {
        // console.log(this.vac)
        var abcs = this.vac.indexOf(',')
        
        this.savedata.templetseData = this.vac.substring(abcs + 1)
        // this.data =   this.dataURLtoBlob('data:text/plain;base64,'+this.savedata.templeteData)
        //  console.log(this.data)
      }

    };


    if (file) {
      reader.readAsDataURL(file);
    }
  }

   dataURLtoBlob(dataurl) {
    var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
    while(n--){
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new Blob([u8arr], {type:mime});
}
  changeFunction(event) {
    console.log(event )

    this.emservices.displaytempletByFilename(event).subscribe(res =>{this.test1 = this.sanitizer.bypassSecurityTrustHtml(res); 
    
   })


  }
}
