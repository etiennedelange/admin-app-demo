import { trigger, transition, query, style, stagger, animate, state } from '@angular/animations';
import { Component, NgZone, OnInit } from '@angular/core';
import { AnimationItem } from 'lottie-web';
import { AnimationOptions } from 'ngx-lottie';
import { AuthenticationService } from '../shared/services/authentication.service';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.less'],
    animations: [
        trigger('fade', [
            state('in', style({ opacity: '1' })),
            transition('* <=> *', [
                animate('.3s ease-in', style({ opacity: '0' }))
            ])
        ])
    ]
})
export class HomeComponent implements OnInit {
    constructor(private authenticationService: AuthenticationService) { }

    options: AnimationOptions = {
        'path': './assets/icons8-services.json'
    };

    public userFullName: string;

    ngOnInit(): void {
        this.authenticationService.authenticationChanged().subscribe((user) => this.userFullName = user.fullName);
    }

    // animationCreated(animationItem: AnimationItem): void {
    //     console.log(animationItem);
    // }

    // onLoopComplete(): void {
    //     NgZone.assertNotInAngularZone();
    //     console.log(NgZone.isInAngularZone()); // false
    // }
}
