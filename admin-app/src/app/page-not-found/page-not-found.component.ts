import { Component, OnInit } from '@angular/core';
import { AnimationItem } from 'lottie-web';
import { AnimationOptions } from 'ngx-lottie';

@Component({
    selector: 'app-page-not-found',
    templateUrl: './page-not-found.component.html',
    styleUrls: ['./page-not-found.component.less']
})
export class PageNotFoundComponent implements OnInit {
    constructor() { }

    options: AnimationOptions = {
        'path': './assets/404.json'
    };

    ngOnInit(): void {
    }

    animationItem: AnimationItem;
    animationCreated(animationItem: AnimationItem) {
        this.animationItem = animationItem;
    }

    animationCompleted() {
        //this.animationItem.stop();
    }
}
