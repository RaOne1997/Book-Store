import { mapEnumToOptions } from "@abp/ng.core";

export enum titleType{
    Mr=1,
    Ms=2,
    Mrs=3
    
}

export const titleTypeOptions = mapEnumToOptions(titleType);