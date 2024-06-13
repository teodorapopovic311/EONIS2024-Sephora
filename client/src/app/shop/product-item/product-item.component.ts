import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { AdminService } from 'src/app/shared/admin/admin.service';
import { Product } from 'src/app/shared/models/products';
import { ShopComponent } from '../shop.component';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent {
  @Input() product?: Product;

  constructor(private basketService: BasketService, private adminService: AdminService, private shopComponent: ShopComponent, private accountService: AccountService) {}

  addItemToBasket() {
    this.product && this.basketService.addItemToBasket(this.product);
  }

  
  isAdmin() {   
    return this.accountService.hasRoleAdmin();
  }

  deleteProductItem(id:number) {
    console.log("deleteProductItem", id);
    this.adminService.deleteProduct(id).subscribe({

      next: () => {

        this.shopComponent.ngOnInit();

      }

    });
  }
  

}


