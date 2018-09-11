import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { OnChanges } from '@angular/core';
import { SimpleChanges } from '@angular/core';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.css']
})
export class DatePickerComponent implements OnChanges {
  @Output() onDateSelected: EventEmitter<Event> = new EventEmitter<Event>();
  @Input() placeHolder: string;
  @Input() displayText: NgbDate;
  constructor() { }

  ngOnChanges(changes: SimpleChanges) {
    debugger
  }
  onDateSelect($event: Event) {
    this.onDateSelected.emit($event);
  }
}
