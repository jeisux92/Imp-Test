import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IProduct } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private httpClient: HttpClient) { }
  get getToken(){
    return localStorage.getItem('token')
  }
  getProductsAsync(): Observable<IProduct[]> {
    return this.httpClient.get<IProduct[]>(`${environment.apiUrl}/products`)
  }

  updateProductAsync(id: number, product: IProduct) {
    return this.httpClient.put(`${environment.apiUrl}/products/${id}`, product, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.getToken}`)
    })
  }
  createProductAsync(product: IProduct) {
    return this.httpClient.post(`${environment.apiUrl}/products`, product,{
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.getToken}`).set('Content-Type', 'application/json').set('Accept', 'application/json')
    })
  }
}
