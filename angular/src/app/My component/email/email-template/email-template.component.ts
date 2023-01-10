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
  savedata: EmailtemplateDTO 
  form: FormGroup;
  emaildata: EmailData
  text: string;
  ngAfterViewInit(): void {
  
   


  }
  constructor(private emservices: EmailService, private http: HttpClient,
    private fb: FormBuilder,) {

  }

  createBook() {
    this.buildForm(); // add this line
    this.isModalOpen = true;
  }
  buildForm() {
    this.form = this.fb.group({
      Templatename: ['', Validators.required],
      // uplodeTemplateFile: [null, Validators.required],
      subject: [null, Validators.required],
      isActive: [null, Validators.required],
      TemplateTYpe: [null, Validators.required]
    });
  }

  handleFileInput(e: any) {
    this.Tempdata = e?.target?.files[0];
  }
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




console.log(this.form.value)
    //  this.form.value.uplodeTemplateFile= this.Tempdata
    // this.savedata.isActive = this.form.value.isActive
    // this.savedata.uplodeTemplateFile = this.Tempdata
    // this.savedata.templateName = this.form.value.Templatename
    // this.savedata.templateType = this.form.value.TemplateTYpe

    // console.log(this.savedata)
    // const formData: FormData = new FormData();
    // formData.append('uplodeTemplateFile', this.Tempdata);
    // formData.append('TemplateName', String(this.form.value.Templatename));
    // formData.append('TemplateType', String(this.form.value.TemplateTYpe));
    // formData.append('isActive', String(this.form.value.isActive));


    //  console.log(formData.get('templateName'))




    // var acb = this.emservices.Addroom(formData).subscribe((val) => { console.log(val) }
    
    
    
    // );
    console.log()
    this.isModalOpen = false;
    this.form.reset();
    // this.list.get();

  }
  changeFunction(event) {

    // this.emservices.displaytempletByFilename(event).subscribe(res =>{this.test1 = this.sanitizer.bypassSecurityTrustHtml(res);})


  }
}
