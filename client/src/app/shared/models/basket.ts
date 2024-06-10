import * as cuid from "cuid"

export interface Basket {
    id: string
    items: BasketItem[]
  }
  
  export interface BasketItem {
    id: number
    ProductName: string
    Price: number
    Quantity: number
    ImageUrl: string
    Brand: string
    ProductType: string
  }

  export class Basket implements Basket {
    id = cuid();
    items: BasketItem[] = [];
  }

  export interface BasketTotals {
    shipping: number;
    subtotal: number;
    total: number;
  }