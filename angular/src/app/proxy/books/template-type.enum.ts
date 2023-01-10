import { mapEnumToOptions } from '@abp/ng.core';

export enum TemplateType {
  Forgotpassword = 0,
  News = 1,
  WelcomeMessage = 2,
  CompleteCorce = 3,
  PaymentCOmplete = 4,
}

export const templateTypeOptions = mapEnumToOptions(TemplateType);
