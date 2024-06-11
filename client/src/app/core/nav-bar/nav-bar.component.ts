import { Component } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketItem } from 'src/app/shared/models/basket';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  Items: BasketItem[];
  constructor(public basketService: BasketService, public accountService: AccountService) {
    this.Items = [];
  }

  getCount() {
    return this.Items.reduce((sum, Item) => sum + Item.quantity, 0);
  }
}
