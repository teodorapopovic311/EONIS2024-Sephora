import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { CheckoutRoutingModule } from './check-routing.module';



@NgModule({
  declarations: [
    CheckoutComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule
  ]
})
export class CheckoutModule { }
