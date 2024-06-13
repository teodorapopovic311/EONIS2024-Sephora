import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';

import { ShopComponent } from './shop.component';
import { EditProductComponent } from './edit-product/edit-product.component';

const routes: Routes = [
  {path: '', component: ShopComponent},
  {path: 'edit/:id', component: EditProductComponent, data: {breadcrumb: {alias: 'editProduct'}}},
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class EditProductRoutingModule { }