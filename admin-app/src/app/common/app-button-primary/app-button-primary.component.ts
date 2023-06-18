import { Component } from '@angular/core';
import { AppButtonComponent } from '../app-button/app-button.component';

@Component({
    selector: 'app-button-primary',
    templateUrl: './app-button-primary.component.html'
})
export class AppButtonPrimaryComponent extends AppButtonComponent {
    constructor() {
        super();
    }
}
