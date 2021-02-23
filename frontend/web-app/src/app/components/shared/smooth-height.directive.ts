import { Directive, OnChanges, Input, HostBinding, ElementRef } from '@angular/core';

@Directive({
    // eslint-disable-next-line @angular-eslint/directive-selector
    selector: '[smoothHeight]',
    // eslint-disable-next-line @angular-eslint/no-host-metadata-property
    host: { '[style.display]': '"block"', '[style.overflow]': '"hidden"' }
})
export class SmoothHeightAnimDirective implements OnChanges {

    @Input() smoothHeight: boolean;
    public pulse: boolean;
    public startHeight: number;

    constructor(private element: ElementRef) { }

    @HostBinding('@grow')
    public get grow(): { value: boolean; params: { startHeight: number } } {
        return { value: this.pulse, params: { startHeight: this.startHeight } };
    }

    public setStartHeight(): void {
        this.startHeight = this.element.nativeElement.clientHeight;
    }

    public ngOnChanges(): void {
        this.setStartHeight();
        this.pulse = !this.pulse;
    }

}
