import { Component, OnInit } from '@angular/core';
import { Order } from '../shared/models/order';
import { OrdersService } from './orders.service';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  orders: Order[] = [];

  constructor(private orderService: OrdersService, private accountService: AccountService) { }

  ngOnInit(): void {
    //this.getOrders();

    if(this.accountService.hasRoleAdmin()) {
      this.getOrdersAll();
    }else{
      this.getOrders();
    }
    //this.getOrdersAll();
  }
  
  getOrders() {
    this.orderService.getOrdersForUser().subscribe({
      next: orders => this.orders = orders
    })
  }

  getOrdersAll() {
    this.orderService.getOrdersAll().subscribe({
      next: orders => this.orders = orders
    })
  }
}