import { Component, OnInit, Input, Self, Optional, Output, EventEmitter, forwardRef } from '@angular/core';
import { ControlValueAccessor, NgControl, FormControl, NG_VALUE_ACCESSOR, } from '@angular/forms';

@Component({
    selector: 'app-form-field',
    templateUrl: './app-form-field.component.html',
    styleUrls: ['./app-form-field.component.less']
})
export class AppFormFieldComponent implements OnInit, ControlValueAccessor {

    constructor(@Optional() readonly ngControl: NgControl) {
        this.ngControl.valueAccessor = this;
    }

    @Input() disabled: boolean;
    @Input() readonly: boolean;
    @Input() label: string;
    @Input() placeholder: string = '';
    @Input() type: 'checkbox' | 'text' | 'email' | 'password' = 'text';
    @Input() hint?: string;
    @Input() required: boolean;
    @Input() id: string;
    @Input() name: string;
    @Input() pattern: string;
    @Input() floatLabel: string;
    @Input() autocomplete: string = 'off';
    @Output() blur: EventEmitter<any> = new EventEmitter();

    public formControl: FormControl;

    // TODO: Create separate components for each input type
    public getErrorMessage() {
        if (this.formControl) {
            if (this.formControl.hasError('required')) {
                return "Field is required";
            } else if (this.formControl.hasError('email')) {
                return "Not a valid email";
            }
            return "Field is invalid";
        }
    }

    ngOnInit() {
        this.formControl = <FormControl>this.ngControl.control;
    }

    public onChange: (_: any) => void;
    public onTouched: () => void;
    public value: string;

    writeValue(value: any): void {
        this.value = value;
        //this.onChange(value);
    }

    registerOnChange(fn: any): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }
}