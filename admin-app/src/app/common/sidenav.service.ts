import { Injectable } from "@angular/core";
import { MatSidenav } from "@angular/material/sidenav";

@Injectable({
    providedIn: "root"
})
export class SidenavService {
    constructor() {
    }

    private rightSidenav: MatSidenav
    private leftSidenav: MatSidenav

    public setRightSidenav(sidenav: MatSidenav) {
        this.rightSidenav = sidenav;
    }

    public setLeftSidenav(sidenav: MatSidenav) {
        this.leftSidenav = sidenav;
    }

    public toggleRightSidenav(isOpen?: boolean) {
        this.rightSidenav.toggle(isOpen);
        localStorage.setItem('sidenavRightState', `${this.rightSidenav.opened}`)
    }

    public toggleLeftSidenav(isOpen?: boolean) {
        this.leftSidenav.toggle(isOpen);
    }

    public toggleLastKnownStateForRightSidenav() {
        this.toggleRightSidenav(localStorage.getItem('sidenavRightState') === "true");
    }

    public closeSideNavs(loggingOut?: boolean) {
        let stateBeforeLoggingOut = localStorage.getItem('sidenavRightState');

        this.toggleRightSidenav(false);
        this.toggleLeftSidenav(false);

        if (loggingOut) {
            localStorage.setItem('sidenavRightState', stateBeforeLoggingOut)
        }
    }

    public isRightSidenavOpen(): boolean {
        return this.rightSidenav?.opened;
    }
}