import * as cuid from "cuid"
  
  export interface BasketItem {
    id: number
    productName: string
    price: number
    quantity: number
    imageUrl: string
    brand: string
    productType: string
  }

  export interface Basket {
    id: string
    items: BasketItem[]
    ClientSecret?: string;
    PaymentIntentId?: string,
    deliveryMethodId?: number;
    shippingPrice: number;
  }

  export class Basket implements Basket {
    id = cuid();
    items: BasketItem[] = [];
    shippingPrice= 0;
  }

  export interface BasketTotals {
    shipping: number;
    subtotal: number;
    total: number;
  }

  