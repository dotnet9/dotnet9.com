import { FormRules } from "element-plus"
import { Ref, ref } from "vue"


export interface SimpleFormModel {
    /**
     * 名称
     */
    name:string
    type: ControlName
    label: string,
    value?: any
    placeholder?: string,
    option?: SimpleControlType,
    selectOption:any
    datePickerOption:datePickerOption
}


export type ControlName = 'input' | 'number' | 'time' | 'select' | 'switch';

export class BaseOption {
    control: ControlName = 'input';
}

export class InputOption extends BaseOption {
    control: ControlName = 'input';
}

export class SelectOption extends BaseOption {
    control: ControlName = 'select'
    items?: { label: string, value: any }[]
}
export interface datePickerOption extends BaseOption {
    type: 'year' | 'month' | 'date' | 'datetime' | 'week' | 'datetimerange' | 'daterange'
    format?: string
}

export type SimpleControlType = SelectOption | InputOption //| DateTimeOption
