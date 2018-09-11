import { Injectable,OnInit } from '@angular/core';
import { DataRow } from 'src/app/models/data-row.model';
import { Voucher } from '../models/voucher.model';
import { CommonService } from '../services/common.service';
import { TableColumn } from '../models/table-column.model';
import { DataType } from '../enums/data-type.enum';

@Injectable({
  providedIn: 'root'
})
export class VoucherService{
  private voucherParameters: TableColumn[];
  private dataRows: DataRow[];
  private vouchers: Voucher[];
  constructor(public commonService: CommonService) {
    this.vouchers = [];
    this.dataRows = [];
  }
  public getVoucherParameters(): TableColumn[] {
      this.voucherParameters = [new TableColumn("Customer Name", DataType.String),
        new TableColumn("Item Catergory", DataType.String),
        new TableColumn("Item", DataType.String),
        new TableColumn("Date", DataType.Date),
        new TableColumn("Amount", DataType.Number),
        new TableColumn("Voucher Type", DataType.String)];
    return this.voucherParameters
  }
  public getVouchers(): DataRow[] {
    //this.dataRows = [new DataRow(["Customer Name", "Item Catergory", "Item", "Date", "Amount", "Voucher Type"])];
    return this.dataRows;
  }

  public getCount(): number {
    //this.dataRows = [new DataRow(["Customer Name", "Item Catergory", "Item", "Date", "Amount", "Voucher Type"])];
    return this.dataRows.length;
  }
  public saveVoucher(voucher: Voucher): boolean {

    this.dataRows.push(this.mapVoucherToDataRow(voucher));
    this.vouchers.push(voucher);
    return true;
  }
  public mapVoucherToDataRow(voucher: Voucher): DataRow {
    let datarow: DataRow = new DataRow([]);
    datarow.data.push(voucher.customerName ? voucher.customerName : '');
    datarow.data.push(voucher.itemCategory ? voucher.itemCategory : '');
    datarow.data.push(voucher.item ? voucher.item: '');
    datarow.data.push(voucher.date ? this.commonService.ConvertNgbDateToString(voucher.date) : '');
    datarow.data.push(voucher.amount ? voucher.amount : '');
    datarow.data.push(voucher.voucherType ? voucher.voucherType : '');
    return datarow;
  }
}
