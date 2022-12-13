import type { EmployeeDTO } from './dto/models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EmployeeServicesService {
  apiName = 'Default';
  

  create = (input: EmployeeDTO) =>
    this.restService.request<any, EmployeeDTO>({
      method: 'POST',
      url: '/api/app/employee-services',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/employee-services/${id}`,
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, EmployeeDTO>({
      method: 'GET',
      url: `/api/app/employee-services/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<EmployeeDTO>>({
      method: 'GET',
      url: '/api/app/employee-services',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: EmployeeDTO) =>
    this.restService.request<any, EmployeeDTO>({
      method: 'PUT',
      url: `/api/app/employee-services/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
