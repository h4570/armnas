import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'size' })
export class SizePipe implements PipeTransform {

    transform(size: number, ...args: any[]): string {
        let letter = 'M';
        if (size > 1000000) {
            letter = 'T';
            size /= 1000000;
        } else if (size > 1000) {
            letter = 'G';
            size /= 1000;
        }
        const finalSize = size.toFixed(1);
        return `${finalSize}${letter}`;
    }

}
