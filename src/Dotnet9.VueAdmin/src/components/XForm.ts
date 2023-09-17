export interface XForm {

    label: string

    option?: XFormType

}

export type XFormType = XInput | XSelect;


export interface XInput {
    value:string
}

export interface XSelect {
    option:[]
}