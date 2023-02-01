import type { FullAuditedEntityDto } from '@abp/ng.core';

export interface CreadeProductDto {
  code: string;
  name: string;
  expiredate: string;
  isAvailable: boolean;
  price: number;
  quentity: number;
}

export interface ProductDto extends FullAuditedEntityDto<string> {
  code?: string;
  name?: string;
  expiredate?: string;
  isAvailable: boolean;
  price: number;
  quentity: number;
}

export interface UpdareProductDto {
  code: string;
  name: string;
  expiredate: string;
  isAvailable: boolean;
  price: number;
  quentity: number;
}
