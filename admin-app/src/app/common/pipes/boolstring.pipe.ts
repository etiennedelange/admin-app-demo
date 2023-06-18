import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'boolstring'
})
export class BoolStringPipe implements PipeTransform {
    transform(value: any): string {
        if (typeof value === "boolean") {
            return value ? "Yes" : 'No'
        } else if (value === null || value === undefined) {
            return 'No'
        }
        return value;
    }
}