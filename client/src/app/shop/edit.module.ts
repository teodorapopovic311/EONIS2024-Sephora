import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule } from '@angular/router';
import { ShopRoutingModule } from './shop-routing.module';
import { EditProductComponent } from './edit-product/edit-product.component';
import { EditProductRoutingModule } from './edit-product-routing.module';



@NgModule({
  declarations: [
    EditProductComponent,
    
    
  ],
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule,
    EditProductRoutingModule
    
  ],
  exports: [
    
  ]

})
export class EditModule { }
