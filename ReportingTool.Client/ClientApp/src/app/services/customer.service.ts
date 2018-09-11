import { Injectable } from '@angular/core';
import { TypeHeadMasterData } from '../models/type-head-master-data.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor() { }
  public getCustomers(): TypeHeadMasterData[] {
    let customers: TypeHeadMasterData[] = [new TypeHeadMasterData(1, 'A'), new TypeHeadMasterData(2, 'B')];
    return customers;
  }
}
