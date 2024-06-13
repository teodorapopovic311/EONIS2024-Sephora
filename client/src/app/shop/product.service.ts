import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
 
@Injectable({
  providedIn: 'root'
})
export class ProductService {
    baseUrl = 'http://localhost:5001/api/';
 
  constructor(private http: HttpClient) { }
 
  getProduct(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }
 
  updateProduct(id: number, product: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, product);
  }

  addProduct(product: any): Observable<any> {
    return this.http.post<any>(this.baseUrl, product);
  }
}