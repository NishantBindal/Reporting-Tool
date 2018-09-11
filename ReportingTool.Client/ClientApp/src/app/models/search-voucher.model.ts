import { NgbDate } from "@ng-bootstrap/ng-bootstrap";

export class SearchVoucher {
  public customerId: number;
  public itemCategoryId: number;
  public itemid: number;
  public minDate: NgbDate;
  public maxDate: NgbDate;
  public voucherType: string;
  public minAmount: string;
  public maxAmount: string;
  public itemCategory: string;
  public customerName: string;
  public item: string;
  constructor() {
    this.minDate = new NgbDate(undefined, undefined, undefined);
    this.maxDate = new NgbDate(undefined, undefined, undefined);
  }
}
