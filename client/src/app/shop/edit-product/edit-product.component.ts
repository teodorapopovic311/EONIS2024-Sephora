import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProductService } from '../product.service';
import { ActivatedRoute, Router } from '@angular/router';

 

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
  //productForm: FormGroup | undefined;
  //productId: number | undefined;
  productForm: FormGroup = new FormGroup({});
  productId: number =  0;
  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

 

  ngOnInit(): void {
    const productId = this.route.snapshot.params['id'];
    this.productService.getProduct(productId).subscribe(product => {
      this.productForm = this.fb.group({
        name: [product.name, Validators.required],
        price: [product.price, Validators.required],
        imageUrl: [product.imageUrl, Validators.required]
      });
    });
  }

 

  onSubmit() {
    if (this.productForm.valid) {
      this.productService.updateProduct(this.productId, this.productForm.value).subscribe(() => {
        this.router.navigate(['/products']);
      });
    }
  }
}


