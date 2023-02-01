import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ImportuserService {
  apiName = 'Default';
  

  readexcelfile = () =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/importuser/readexcelfile',
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
