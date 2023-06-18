import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { LoadingBarService } from 'src/app/shared/services/loading-bar.service';

@Component({
    selector: 'app-loading-bar',
    templateUrl: './loading-bar.component.html',
    styleUrls: ['./loading-bar.component.less']
})
export class LoadingBarComponent implements OnInit {
    constructor(private loadingBarService: LoadingBarService) { }

    loadingState$: Observable<boolean>;

    ngOnInit(): void {
        this.loadingState$ = this.loadingBarService.getLoadingState();
    }
}