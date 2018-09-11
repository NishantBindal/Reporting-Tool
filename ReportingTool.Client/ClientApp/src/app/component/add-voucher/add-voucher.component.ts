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

@Component({
  selector: 'app-add-voucher',
  templateUrl: './add-voucher.component.html',
  styleUrls: ['./add-voucher.component.css']
})
export class AddVoucherComponent implements OnInit {
  public itemsCategories: TypeHeadMasterData[];
  public items: TypeHeadMasterData[];
  public columns: TableColumn[];
  public customers: TypeHeadMasterData[];
  public dataRows: DataRow[];
  public voucher: Voucher;
  public voucherCount: number;
  public addAction:boolean
  constructor(private voucherService: VoucherService, private customerService: CustomerService, private itemService: ItemService, private reportingService: ReportingService) { }

  ngOnInit() {
    this.columns = this.voucherService.getVoucherParameters();
    this.dataRows = this.voucherService.getVouchers();
    this.customers = this.customerService.getCustomers();
    this.itemsCategories = this.itemService.getItemCategories();
    this.items = this.itemService.getItemByCategories(1);
    this.voucher = new Voucher();
    this.addAction = true;
  }
  onDateSelected(event: NgbDate) {
    debugger
    this.voucher.date = event;
  }
  selectCustomer(customer: TypeHeadMasterData) {
    this.voucher.customerId = customer.id
    this.voucher.customerName = customer.name;
  }
  selectItemCategory(itemCategory: TypeHeadMasterData) {
    this.voucher.itemCategoryId = itemCategory.id;
    this.voucher.itemCategory = itemCategory.name;
  }
  selectItem(item: TypeHeadMasterData) {
    this.voucher.itemid = item.id;
    this.voucher.item = item.name;
  }
  saveCreditVoucher() {
    this.voucher.voucherType = VoucherType.Credit;
    let clonedVoucher = _.cloneDeep(this.voucher);
    this.voucherService.saveVoucher(clonedVoucher);
    this.voucherCount = this.voucherService.getCount();
  }
  saveDebitVoucher() {
    this.voucher.voucherType = VoucherType.Debit;
    let clonedVoucher = _.cloneDeep(this.voucher);
    this.voucherService.saveVoucher(clonedVoucher);
    this.voucherCount = this.voucherService.getCount();
  }
  clearVoucher() {
    this.voucher = new Voucher();
  }
  downloadPdf(): void {
    this.reportingService.generateReport(ReportType.Pdf);
  }
}
