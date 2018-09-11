import { Injectable } from '@angular/core';
import { TypeHeadMasterData } from '../models/type-head-master-data.model';

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  private itemCategories: TypeHeadMasterData[]
  constructor() { }
  public getItemCategories(): TypeHeadMasterData[] {
    this.itemCategories = [new TypeHeadMasterData(1, "A"), new TypeHeadMasterData(2, "B")];
    return this.itemCategories;
  }
  public getItemByCategories(itemCategoryId: number): TypeHeadMasterData[] {
    return [new TypeHeadMasterData(1, "A"), new TypeHeadMasterData(2, "B")];
  }
}
