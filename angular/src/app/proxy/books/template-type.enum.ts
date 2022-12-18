import { mapEnumToOptions } from '@abp/ng.core';

export enum TemplateType {
  Forgotpassword = 0,
  News = 1,
  WelcomeMessage = 2,
  CompleteCorce = 3,
  PaymentCOmplete = 4,
}

export const templateTypeOptions = mapEnumToOptions(TemplateType);


export enum TitleType {
  Mr = 1,
  Ms = 2,
  Mrs = 3,
}

export const titleTypeOptions = mapEnumToOptions(TitleType);
