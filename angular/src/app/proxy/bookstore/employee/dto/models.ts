import type { AuditedEntityDto } from '@abp/ng.core';

export interface EmployeeDTO extends AuditedEntityDto<string> {
  name?: string;
}
