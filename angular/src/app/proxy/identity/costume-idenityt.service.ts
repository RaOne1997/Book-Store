import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { GetIdentityUsersInput, IdentityUserCreateDto, IdentityUserDto, IdentityUserUpdateDto } from '../volo/abp/identity/models';

@Injectable({
  providedIn: 'root',
})
export class CostumeIDenitytService {
  apiName = 'Default';
  

  create = (input: IdentityUserCreateDto) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'POST',
      url: '/api/app/costume-iDenityt',
      body: input,
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'GET',
      url: `/api/app/costume-iDenityt/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetIdentityUsersInput) =>
    this.restService.request<any, PagedResultDto<IdentityUserDto>>({
      method: 'GET',
      url: '/api/app/costume-iDenityt',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: IdentityUserUpdateDto) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'PUT',
      url: `/api/app/costume-iDenityt/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
