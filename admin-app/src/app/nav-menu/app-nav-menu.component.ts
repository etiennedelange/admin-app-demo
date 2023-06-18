import { Component, OnInit } from '@angular/core';
import { UserRole } from '../shared/user-role';
import { AuthenticationService } from '../shared/services/authentication.service';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './app-nav-menu.component.html',
    styleUrls: ['./app-nav-menu.component.less']
})
export class AppNavMenuComponent implements OnInit {

    private allMenuItems: NavMenuItem[] = [];
    public authorisedMenuItems: NavMenuItem[] = [];

    constructor(private authenticationService: AuthenticationService) {
    }

    ngOnInit(): void {
        this.authenticationService.loginStateChanged().subscribe((loggedIn: boolean) => {
            if (loggedIn) {
                this.authoriseMenuItems();
            }
        });

        this.allMenuItems = [
            {
                displayName: "Firms",
                routerLink: "attorneys",
                roles: [UserRole.Administrator, UserRole.ManageAttorneys, UserRole.ViewAttorneys]
            },
            {
                displayName: "Settings",
                routerLink: "settings",
                roles: [UserRole.Administrator, UserRole.ManageAttorneys, UserRole.ViewAttorneys]
            }, {
                displayName: "Reports",
                routerLink: "reports",
                roles: [UserRole.Administrator, UserRole.ManageAttorneys, UserRole.ViewAttorneys]
            },
            {
                displayName: "Users",
                routerLink: "users",
                roles: [UserRole.Administrator, UserRole.ManageUsers, UserRole.ViewUsers]
            },
            {
                displayName: "Template Files",
                routerLink: "templatefiles",
                roles: [UserRole.Administrator, UserRole.ManageTemplates, UserRole.ViewTemplates]
            },
            {
                displayName: "Platform Versions",
                routerLink: "templateversions",
                roles: [UserRole.Administrator, UserRole.ManageTemplates, UserRole.ViewTemplates]
            }
        ];

        this.authoriseMenuItems();
    }

    private authoriseMenuItems() {
        this.authorisedMenuItems = this.allMenuItems.filter(x => this.userHasSomeRoles(x));
    }

    public isLoggedIn(): boolean {
        return this.authenticationService.isLoggedIn();
    }

    public userHasSomeRoles(menuItem: NavMenuItem): boolean {
        return this.authenticationService.hasSomeRoles(menuItem.roles);
    }
}

export class NavMenuItem {
    public displayName: string;
    public routerLink: string;
    public roles: UserRole[];
    constructor() { }
}