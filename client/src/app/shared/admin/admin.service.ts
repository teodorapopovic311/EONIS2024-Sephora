import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Brand } from '../models/brand';
import { Pagination } from '../models/pagination';
import { Product } from '../models/products';
import { ShopParams } from '../models/shopParams';
import { Type } from '../models/type';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = 'http://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.brandId > 0) params = params.append('brandId', shopParams.brandId);
    if (shopParams.typeId) params = params.append('typeId', shopParams.typeId);
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);
    if (shopParams.search) params = params.append('search', shopParams.search);

    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products', {params});
  }

  getProduct(id: number) {
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  /*deleteProduct(id: number) {
    console.log("deleteProductItem-adminService", id);
    return this.http.delete<Product>(this.baseUrl + 'products/' + id);
  }*/
    deleteProduct(id: number) {

      return this.http.delete(this.baseUrl + 'products/' + id);
  
    }
  
  

  getBrands() {
    return this.http.get<Brand[]>(this.baseUrl + 'products/brands');
  }

  getTypes() {
    return this.http.get<Type[]>(this.baseUrl + 'products/types');
  }

}
