import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BlobDto, GetBlobRequestDto, SaveBlobInputDto } from '../blob-storage/models';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  apiName = 'Default';
  

  deleteBlob = (input: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/file/blob',
      params: { input },
    },
    { apiName: this.apiName });
  

  getBlob = (input: GetBlobRequestDto) =>
    this.restService.request<any, BlobDto>({
      method: 'GET',
      url: '/api/app/file/blob',
      params: { name: input.name },
    },
    { apiName: this.apiName });
  

  isExistByInput = (input: string) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/file/is-exist',
      params: { input },
    },
    { apiName: this.apiName });
  

  saveBlob = (input: SaveBlobInputDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/file/save-blob',
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
