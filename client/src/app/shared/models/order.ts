import { Address } from "./user";

export interface OrderToCreate {
    BasketId: string;
    DeliveryMethodId: number;
    ShipToAddress: Address;

}

export interface OrderItem {
    ProductItemId: number;
    ProductName: string;
    ImageUrl: string;
    Price: number;
    Quantity: number;
}

export interface Order {
    Id: number;
    BuyerEmail: string;
    OrderDate: Date;
    ShipTpAddress: Address;
    DeliveryMethod: string;
    ShippingPrice: number;
    OrderItems: OrderItem[];
    subtotal: number;

}