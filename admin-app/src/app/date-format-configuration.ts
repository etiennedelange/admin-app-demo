import { Platform } from '@angular/cdk/platform';
import { Provider } from '@angular/core';
import { DateAdapter, MatDateFormats, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { AppDayJsDateAdapter } from './app-dayjs-date.adapter';

const APP_DATE_FORMAT: MatDateFormats = {
    parse: {
        dateInput: 'DD MMM YYYY'
    },
    display: {
        dateInput: 'DD MMM YYYY',
        monthYearLabel: 'MMMM YYYY',
        dateA11yLabel: 'DD-MMM-YYYY',
        monthYearA11yLabel: 'MMMM YYYY'
    }
};

export const DateFormatProviders: Provider[] = [
    { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMAT },
]

export const AppDateAdapterProvider: Provider = {
    provide: DateAdapter,
    useClass: AppDayJsDateAdapter,
    deps: [MAT_DATE_LOCALE, Platform]
}