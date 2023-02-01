import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PaypallApiAppServicesService {
  apiName = 'Default';
  

  redirectUrlasync = () =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/paypall-api-app-services/redirect-urlasync',
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
