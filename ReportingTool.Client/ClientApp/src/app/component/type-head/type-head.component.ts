import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { NgbTypeaheadConfig } from '@ng-bootstrap/ng-bootstrap';
import {  } from '@angular/core';
import { TypeHeadMasterData } from '../../models/type-head-master-data.model';
import { OnChanges } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
  selector: 'app-type-head',
  templateUrl: './type-head.component.html',
  styleUrls: ['./type-head.component.css'],
  providers: [NgbTypeaheadConfig]
})
export class TypeHeadComponent implements OnChanges{
  public selectedId: number;
  @Input() data: TypeHeadMasterData[];
  @Input() required: boolean;
  @Input() placeHolder: string;
  @Input() id: string; 
  @Input() validationMessage: string;
  @Input() displayTextId: number;
  displayText: string;
  @Output() onSelect: EventEmitter<TypeHeadMasterData> = new EventEmitter<TypeHeadMasterData>();
  value:string
  constructor(config: NgbTypeaheadConfig) {
    // customize default values of typeaheads used by this component tree
    config.showHint = true;
  }
  ngOnChanges() {
    if (this.displayTextId == undefined)
      this.displayText = '';
  }
  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map(term => term.length < 0 ? []
        : this.data ? this.data.filter(v => v.name.toLowerCase().startsWith(term.toLocaleLowerCase())).splice(0, 10) : [])
    );
  formatter = (x: TypeHeadMasterData) => x.name;
  onSelectTypeahead(data: TypeHeadMasterData) {
    debugger
    this.selectedId = data.id;
    this.onSelect.emit(data);
  }
}
