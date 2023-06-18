import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
    selector: 'app-button',
    templateUrl: './app-button.component.html'
})
export class AppButtonComponent implements OnInit {
    constructor() {
    }

    @Output() onclick: EventEmitter<any> = new EventEmitter();
    @Input() disabled: boolean = false;
    @Input() text: string = '';

    ngOnInit(): void {
    }
}
