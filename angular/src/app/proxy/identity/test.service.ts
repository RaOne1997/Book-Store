import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { Regristrationdto } from '../regrist/models';
import type { RegisterDto, ResetPasswordDto, SendPasswordResetCodeDto } from '../volo/abp/account/models';
import type { IdentityUserDto } from '../volo/abp/identity/models';

@Injectable({
  providedIn: 'root',
})
export class TestService {
  apiName = 'Default';
  

  register = (input: RegisterDto) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'POST',
      url: '/api/app/test/register',
      body: input,
    },
    { apiName: this.apiName });
  

  registers = (input: Regristrationdto) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'POST',
      url: '/api/app/test/registers-asyncss',
      body: input,
    },
    { apiName: this.apiName });
  

  resetPassword = (input: ResetPasswordDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/test/reset-password',
      body: input,
    },
    { apiName: this.apiName });
  

  resetPasswordtest = (input: ResetPasswordDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/test/reset-passwordtest',
      body: input,
    },
    { apiName: this.apiName });
  

  sendPasswordResetCode = (input: SendPasswordResetCodeDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/test/send-password-reset-code',
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
