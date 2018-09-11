import { NgbDate } from "@ng-bootstrap/ng-bootstrap";

export class Voucher {
  public customerId: number;
  public itemCategoryId: number;
  public itemid: number;
  public date: NgbDate;
  public voucherType: string;
  public amount: string;
  public itemCategory: string;
  public customerName: string;
  public item: string;
  constructor() {
    this.date = new NgbDate(undefined, undefined, undefined);
  }
}
