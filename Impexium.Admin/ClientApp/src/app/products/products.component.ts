import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProductsService } from '../core/products.service';
import { CreateProductComponent } from '../create-product/create-product.component';
import { IProduct } from '../models/product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  displayedColumns: string[] = ['name', 'description', 'quantity', 'actions'];
  dataSource: IProduct[] = [];

  constructor(private productsService: ProductsService, private dialog: MatDialog) { }


  ngOnInit(): void {
    this.productsService.getProductsAsync().subscribe((x: IProduct[]) => this.dataSource = x)
  }

  
  taken = async (id: number): Promise<void> => {
    let bookToUpdate: IProduct | undefined = this.dataSource.find(x => x.id === id);

    await this.productsService.updateProductAsync(id, bookToUpdate).toPromise()
  }

  openCreationModal = (): void => {
    let modal = this.dialog.open(CreateProductComponent)
    modal.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.productsService.getProductsAsync().subscribe((x: IProduct[]) => this.dataSource = x)
      }
    });
  }

  openEditModal = (product: IProduct): void => {
    let modal = this.dialog.open(CreateProductComponent, { data: { ...product } })
    modal.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.productsService.getProductsAsync().subscribe((x: IProduct[]) => this.dataSource = x)
      }
    });
  }
}
