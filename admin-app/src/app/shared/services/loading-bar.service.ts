import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class LoadingBarService {
    private loadingStateSubject: BehaviorSubject<boolean>;

    constructor() {
        this.loadingStateSubject = new BehaviorSubject(false);
    }

    public getLoadingState(): Observable<boolean> {
        return this.loadingStateSubject.asObservable();
    }

    public setLoadingState(loadingState: boolean): void {
        this.loadingStateSubject.next(loadingState);
    }
}
