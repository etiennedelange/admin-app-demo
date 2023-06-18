import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
    selector: 'app-back-button',
    templateUrl: './app-back-button.component.html',
    styleUrls: ['./app-back-button.component.less']
})
export class BackButtonComponent implements OnInit {

    constructor(
        private location: Location) {
    }

    ngOnInit(): void {
    }

    public goBack(): void {
        this.location.back()
    }

}
