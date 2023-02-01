import { mapEnumToOptions } from '@abp/ng.core';

export enum OrderStatus {
  Approved = 0,
  Processing = 1,
  PaymentPending = 2,
  Canceled = 3,
  Shiped = 4,
}

export const orderStatusOptions = mapEnumToOptions(OrderStatus);
