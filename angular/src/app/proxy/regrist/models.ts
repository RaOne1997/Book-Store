import type { RegisterDto } from '../volo/abp/account/models';

export interface Regristrationdto extends RegisterDto {
  name?: string;
  surname?: string;
  gender?: string;
}
