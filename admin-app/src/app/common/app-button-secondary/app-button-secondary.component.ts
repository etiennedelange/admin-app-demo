import { Component } from '@angular/core';
import { AppButtonComponent } from '../app-button/app-button.component';

@Component({
    selector: 'app-button-secondary',
    templateUrl: './app-button-secondary.component.html'
})
export class AppButtonSecondaryComponent extends AppButtonComponent {
    constructor() {
        super();
    }
}
