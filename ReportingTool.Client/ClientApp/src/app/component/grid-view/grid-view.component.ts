import { Component, OnInit,OnChanges } from '@angular/core';
import { Input } from '@angular/core';
import { DataRow } from '../../models/data-row.model';
import { VoucherService } from '../../services/voucher.service';
import { TableColumn } from '../../models/table-column.model';
import { DataType } from '../../enums/data-type.enum';
import { SortOrder } from '../../enums/sort-order.enum';

@Component({
  selector: 'app-grid-view',
  templateUrl: './grid-view.component.html',
  styleUrls: ['./grid-view.component.css']
})
export class GridViewComponent implements OnInit, OnChanges {
  @Input() columns: TableColumn[];
  sortedColumn: TableColumn
  @Input() dataRows: DataRow[];
  @Input() rowCount: boolean = false;
  itemsPerPage: number = 10;
  pageData: DataRow[];
  activePage: number = 1;
  @Input() addAction: boolean;
  constructor() { }

  ngOnInit() {
  }
  ngOnChanges() {
    this.columns.forEach((column, index) => this.sort(column, index));

    this.sliceData(this.activePage);
  }
  sliceData(pageNumber: number) {
    this.pageData = this.dataRows.slice((pageNumber - 1) * this.itemsPerPage, (pageNumber - 1) * this.itemsPerPage + this.itemsPerPage);
  }
  public sortReqested(column: TableColumn, index: number): void {
    column.sort = column.sort == undefined ? SortOrder.Descending : (-(column.sort as number) as SortOrder);
    this.sort(column, index);
    this.sortedColumn = column;
    this.sliceData(this.activePage);
  }
  public sort(column: TableColumn, index: number): void {
    if (this.dataRows) {
      if (column.sort == undefined) 
        return;
      switch (column.type) {
        case DataType.String: {
          this.dataRows.sort((input1, input2) => {
            var value1 = input1.data[index].toLowerCase(), value2 = input2.data[index].toLowerCase();
            if (value1 < value2) return (column.sort as number) * (-1);
            if (value1 > value2) return (column.sort as number) * 1;
            return 0;
          });
          break;
        }
        case DataType.Number: {
          this.dataRows.sort((input1, input2) => {
            var value1 = input1.data[index].toLowerCase(), value2 = input2.data[index].toLowerCase();
            if (value1 < value2) return (column.sort as number) * (-1);
            if (value1 > value2) return (column.sort as number) * 1;
            return 0;
          });
          break;
        }
        case DataType.Date: {
          this.dataRows.sort((input1, input2) => {
            var value1 = new Date(input1.data[index].toLowerCase()).getTime(), value2 = new Date(input2.data[index].toLowerCase()).getTime();
            if (value1 - value2 < 0) return (column.sort as number) * (-1);
            if (value1 - value2 > 0) return (column.sort as number) * 1;
            return 0;
          });
          break
        }
      }
    }
  }
  public isAscendingSortOrder(column: TableColumn): boolean{
    return column.sort != undefined ? column.sort== SortOrder.Ascending && this.sortedColumn.name == column.name : false;
  }

  public isDescendingSortOrder(column: TableColumn): boolean {
    return column.sort != undefined ? column.sort == SortOrder.Descending && this.sortedColumn.name == column.name : false;
  }
  public refreshPageData(pageNumber: number) {
    this.activePage = pageNumber;
    this.sliceData(pageNumber);
  }
  public deleteVoucher(index: number) {
    this.dataRows.splice(index, 1);
    this.sliceData(this.activePage);
  }
}
