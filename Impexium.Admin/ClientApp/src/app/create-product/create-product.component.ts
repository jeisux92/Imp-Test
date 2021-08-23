import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ProductsService } from '../core/products.service';
import { IProduct } from '../models/product';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent implements OnInit {

  form!: FormGroup;
  constructor(private fb: FormBuilder, private productsService: ProductsService, private dialogRef: MatDialogRef<CreateProductComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IProduct) {
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      name: [this.data && this.data.name ? this.data.name : '', [Validators.required]],
      description: [this.data && this.data.description ? this.data.description : '', [Validators.required]],
      quantity: [this.data && this.data.quantity ? this.data.quantity : '', [Validators.required]],
    });
  }
  createOrUpdate = async (): Promise<void> => {
    let product: IProduct = {
      name: this.form.get('name').value,
      description: this.form.get('description').value,
      quantity: this.form.get('quantity').value
    }
    if (this.data) {
      this.data.description = product.description;
      this.data.quantity = product.quantity;
      await this.productsService.updateProductAsync(this.data.id, this.data).toPromise();
    }
    else {
      await this.productsService.createProductAsync(product).toPromise();
    }

    this.dialogRef.close(true)
  }
}
