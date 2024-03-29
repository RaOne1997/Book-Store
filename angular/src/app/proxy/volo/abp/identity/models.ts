import type { ExtensibleFullAuditedEntityDto, ExtensibleObject, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetIdentityUsersInput extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface IdentityUserCreateDto extends IdentityUserCreateOrUpdateDtoBase {
  password: string;
}

export interface IdentityUserCreateOrUpdateDtoBase extends ExtensibleObject {
  userName: string;
  name?: string;
  surname?: string;
  email: string;
  phoneNumber?: string;
  isActive: boolean;
  lockoutEnabled: boolean;
  roleNames: string[];
}

export interface IdentityUserDto extends ExtensibleFullAuditedEntityDto<string> {
  tenantId?: string;
  userName?: string;
  name?: string;
  surname?: string;
  email?: string;
  emailConfirmed: boolean;
  phoneNumber?: string;
  phoneNumberConfirmed: boolean;
  isActive: boolean;
  lockoutEnabled: boolean;
  lockoutEnd?: string;
  concurrencyStamp?: string;
}

export interface IdentityUserUpdateDto extends IdentityUserCreateOrUpdateDtoBase {
  password?: string;
  concurrencyStamp?: string;
}
