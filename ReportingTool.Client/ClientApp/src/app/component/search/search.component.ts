import { Component, OnInit } from '@angular/core';
import * as _ from "lodash";
import { TypeHeadMasterData } from '../../models/type-head-master-data.model';
import { DataRow } from '../../models/data-row.model';
import { VoucherService } from '../../services/voucher.service';
import { CustomerService } from '../../services/customer.service';
import { ItemService } from '../../services/item.service';
import { Voucher } from '../../models/voucher.model';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap/datepicker/ngb-date';
import { VoucherType } from '../../constants/voucher-type.constant';
import { TableColumn } from '../../models/table-column.model';
import { ReportingService } from '../../services/reporting.service';
import { ReportType } from '../../constants/report-type.constant';
import { SearchVoucher } from 'src/app/models/search-voucher.model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  public itemsCategories: TypeHeadMasterData[];
  public items: TypeHeadMasterData[];
  public columns: TableColumn[];
  public customers: TypeHeadMasterData[];
  public dataRows: DataRow[];
  public searchVoucher: SearchVoucher;
  public voucherCount: number;
  public addAction: boolean
  constructor(private voucherService: VoucherService, private customerService: CustomerService, private itemService: ItemService, private reportingService: ReportingService) { }

  ngOnInit() {
    this.columns = this.voucherService.getVoucherParameters();
    this.dataRows = this.voucherService.getVouchers();
    this.customers = this.customerService.getCustomers();
    this.itemsCategories = this.itemService.getItemCategories();
    this.items = this.itemService.getItemByCategories(1);
    this.searchVoucher = new SearchVoucher();
    this.addAction = false;
  }
  onMinDateSelected(event: NgbDate) {
    debugger
    this.searchVoucher.minDate = event;
  }
  onMaxDateSelected(event: NgbDate) {
    debugger
    this.searchVoucher.maxDate = event;
  }
  selectCustomer(customer: TypeHeadMasterData) {
    this.searchVoucher.customerId = customer.id
    this.searchVoucher.customerName = customer.name;
  }
  selectItemCategory(itemCategory: TypeHeadMasterData) {
    this.searchVoucher.itemCategoryId = itemCategory.id;
    this.searchVoucher.itemCategory = itemCategory.name;
  }
  selectItem(item: TypeHeadMasterData) {
    this.searchVoucher.itemid = item.id;
    this.searchVoucher.item = item.name;
  }
  search() {
  }
  clearVoucher() {
    this.searchVoucher = new SearchVoucher();
  }
  downloadPdf(): void {
    this.reportingService.generateReport(ReportType.Pdf);
  }
}
