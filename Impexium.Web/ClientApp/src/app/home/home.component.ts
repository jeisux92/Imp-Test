import { Component } from '@angular/core';
import { ProductsService } from '../core/products.service';
import { IProduct } from '../models/product';

@Component({
  selector: 'app-home',
  styleUrls: ['./home.component.scss'],
  templateUrl: './home.component.html',
})
export class HomeComponent {
  displayedColumns: string[] = ['name', 'description', 'quantity'];
  dataSource: IProduct[] = [];

  constructor(private productsService: ProductsService) { }


  ngOnInit(): void {
    this.productsService.getProductsAsync().subscribe((x: IProduct[]) => this.dataSource = x)
  }
}
