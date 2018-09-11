import { Injectable } from '@angular/core';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor() { }
  public ConvertNgbDateToString(date: NgbDate): string{
    if (date.year != undefined && date.month != undefined && date.day != undefined )
      return date.year + '-' + date.month + '-' + date.day;
    return '';
  }
}
