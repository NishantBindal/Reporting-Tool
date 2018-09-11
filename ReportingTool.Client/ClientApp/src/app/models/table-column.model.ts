import { DataType } from "../enums/data-type.enum";
import { SortOrder } from "../enums/sort-order.enum";

export class TableColumn {
  type: DataType;
  name: string;
  sort: SortOrder;
  constructor(name: string, type: DataType, sortOrder?: SortOrder) {
    this.name = name;
    this.type = type;
    this.sort = sortOrder;
  }
}
