import { Platform } from "@angular/cdk/platform";
import { Injectable } from "@angular/core";
import { NativeDateAdapter } from "@angular/material/core";
import * as dayjs from "dayjs";

@Injectable()
export class AppDayJsDateAdapter extends NativeDateAdapter {

    constructor(matDateLocale: string, platform: Platform) {
        super(matDateLocale, platform);
    }

    parse(value: any): Date | null {
        return dayjs(value).toDate();
    }

    format(date: Date, displayFormat: any): string {
        return dayjs(date).format(displayFormat);
    }
}