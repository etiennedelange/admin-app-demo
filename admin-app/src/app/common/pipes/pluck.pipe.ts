import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'pluck'
})
export class PluckPipe implements PipeTransform {
    transform(value: string[], key: string): string[] {
        return value.map(value => value[key]);
    }
}