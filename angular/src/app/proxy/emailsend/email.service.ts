import type { EmailData, EmailSettingsDTO, EmailtemplateDTO, Templatename } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { InputEmailConfigration } from '../bookstore/email-send/models';

@Injectable({
  providedIn: 'root',
})
export class EmailService {
  apiName = 'Default';
  

  create = (input: InputEmailConfigration) =>
    this.restService.request<any, EmailSettingsDTO>({
      method: 'POST',
      url: '/api/app/email',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/email/${id}`,
    },
    { apiName: this.apiName });
  

  displaytempletByFilename = (filename: string) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/email/displaytemplet',
      params: { filename },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, EmailSettingsDTO>({
      method: 'GET',
      url: `/api/app/email/${id}`,
    },
    { apiName: this.apiName });
  

  getAlltemplatename = () =>
    this.restService.request<any, Templatename[]>({
      method: 'GET',
      url: '/api/app/email/templatename',
    },
    { apiName: this.apiName });
  

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<EmailSettingsDTO>>({
      method: 'GET',
      url: '/api/app/email',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });
  

  sendEmail = (emailData: EmailData) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/email/send-email',
      body: emailData,
    },
    { apiName: this.apiName });
  

  testSendEmail = (emailData: InputEmailConfigration, toemail: string) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/email/test-send-email',
      params: { toemail },
      body: emailData,
    },
    { apiName: this.apiName });
  

  update = (id: string, input: InputEmailConfigration) =>
    this.restService.request<any, EmailSettingsDTO>({
      method: 'PUT',
      url: `/api/app/email/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  uploadFile = (form: EmailtemplateDTO) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/email/upload-file',
      body: form,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
