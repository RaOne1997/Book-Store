import { DOCUMENT } from '@angular/common';
import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
// import { PDFDocument, StandardFonts, rgb } from 'pdf-lib';
import { ScriptService } from '../raza-pay/ScriptService';
declare let fontkit: any;
@Component({
  selector: 'app-createinvoice',
  templateUrl: './createinvoice.component.html',
  styleUrls: ['./createinvoice.component.scss']
})
export class CreateinvoiceComponent implements OnInit {

  list:any;
  constructor(private elRef: ElementRef, private fontoad: ScriptService) {
    fontoad.load('boot')

    this.list=[
      "hello",
      "world"
    ]
  }

  selectFunction(item):string{

    return "--Select Temp--"
  }

  ngOnInit(): void {
    // this.createPdf();
  }

  // async createPdf() {

  //   const exbyte = await fetch('../../assets/Cert.pdf').then((rec) => { return rec.arrayBuffer() })
  //   const exfont = await fetch('../../assets/fonts/GreatVibesRegular.ttf').then((rec) => { return rec.arrayBuffer() })
  //   const pngImageBytes = await fetch('../../assets/images/logo/logo-light2.png').then((res) => res.arrayBuffer())


  //   const pdfDoc = await PDFDocument.load(exbyte)
  //   const jpgImage = await pdfDoc.embedPng(pngImageBytes)
  //   const jpgDims = jpgImage.scale(0.5)
  //   pdfDoc.registerFontkit(fontkit)


  //   const fonts = await pdfDoc.embedFont(exfont)
  //   const pages = pdfDoc.getPages();
  //   const firstp = pages[0]

  //   firstp.drawText('Abhijeet Warade', {
  //     x: 300,
  //     y: 300,
  //     size: 48.3,
  //     font: fonts

  //   })


  //   firstp.drawImage(jpgImage, {
  //     x: firstp.getWidth() / 2 - jpgDims.width / 7,
  //     y: firstp.getHeight() / 2 - jpgDims.height / 2 + 230,
  //     width: 40,
  //     height: 40,
  //   })
  //   firstp.drawLine({
  //     start: { x: 600, y: 280 },
  //     end: { x: 250, y: 280 },
  //     thickness: 1,
  //     opacity: 1,
  //   })
  //   pdfDoc.saveAsBase64({ dataUri: true }).then((a) => {

  //     var div = this.elRef.nativeElement.querySelector('#viwesss');

  //     div.src = a

  //   })



  // }


  saveByteArray(reportName, byte) {
    var blob = new Blob([byte], { type: "application/pdf" });
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    var fileName = reportName;
    link.download = fileName;
    link.click();
  };

  changeFunction(any) {

    console

  }
}





export class filesss {

  file: any


}