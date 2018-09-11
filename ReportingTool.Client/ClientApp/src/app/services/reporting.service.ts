import { Injectable } from '@angular/core';
import * as jsPDF from 'jspdf';
import * as html2canvas from 'html2canvas';
@Injectable({
  providedIn: 'root'
})
export class ReportingService {

  constructor() { }
  generateReport(format: any): boolean {
    if (format != undefined) {
      switch (format) {
        case ReportType.Pdf: 
          this.generatePdf();
          return true;
      }
    }
  }
  generatePdf():void{

    html2canvas(document.getElementById('grid-view')).then(function (canvas) {
      var img = canvas.toDataURL("image/png");
      var doc = new jsPDF();
      doc.addImage(img, 'JPEG', 5, 20);
      doc.save('testCanvas.pdf');
    });
  }

}
