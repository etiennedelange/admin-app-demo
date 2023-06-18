import { Component } from '@angular/core';
import { AppButtonComponent } from '../app-button/app-button.component';

@Component({
    selector: 'app-add-button',
    templateUrl: './app-add-button.component.html',
    styleUrls: ['./app-add-button.component.less']
})
export class AddButtonComponent extends AppButtonComponent {
    constructor() {
        super();
    }
}
