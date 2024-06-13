import { NgModule } from "@angular/core";
import { AddProductComponent } from "./add-product/add-product.component";
import { ReactiveFormsModule } from "@angular/forms";

@NgModule({
    declarations: [
      // other declarations
      AddProductComponent
    ],
    imports: [
      // other imports
      ReactiveFormsModule
    ],
    // providers, bootstrap, etc.
  })
  export class AddModule { }