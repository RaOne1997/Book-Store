import type { FullAuditedEntityDto } from '@abp/ng.core';
import type { OrderStatus } from '../order-s/order-status.enum';

export interface CreateOrderInput {
  customerId: string;
  products: Record<string, number>;
}

export interface OrderDTO extends FullAuditedEntityDto<string> {
  orderDate?: string;
  orderNumber?: string;
  orderStatus: OrderStatus;
  customerID?: string;
  orderLines: OrderLineDto[];
}

export interface OrderLineDto {
  productID?: string;
  orderId?: string;
  _Quentity: number;
}
