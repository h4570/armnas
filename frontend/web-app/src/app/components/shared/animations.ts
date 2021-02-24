import { animate, style, transition, trigger } from '@angular/animations';

export const smoothHeight = trigger('grow', [
    transition('void <=> *', []),
    transition('* <=> *', [style({ height: '{{startHeight}}px', opacity: 100 }), animate('.5s ease')], {
        params: { startHeight: 0 }
    })
]);
