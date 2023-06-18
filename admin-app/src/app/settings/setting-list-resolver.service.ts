import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { ISetting, SettingService } from "./setting.service";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

//TODO: Add unit tests for these
@Injectable({
    providedIn: "root"
})
export class SettingsPageResolver implements Resolve<ISetting[]> {
    constructor(private settingService: SettingService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): ISetting[] | Observable<ISetting[]> | Promise<ISetting[]> {
        return this.settingService.get(0, 10, '', 'asc');
    }
}

@Injectable({
    providedIn: "root"
})
export class SettingCountResolver implements Resolve<number> {
    constructor(private settingService: SettingService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<number> {
        return this.settingService.getTotal();
    }
}

@Injectable({
    providedIn: "root"
})
export class SettingsResolverService implements Resolve<ISetting[]> {
    constructor(private settingService: SettingService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ISetting[]> {
        return this.settingService.getAll();
    }
}

@Injectable({
    providedIn: "root"
})
export class SettingDetailResolverService implements Resolve<ISetting> {
    constructor(private settingService: SettingService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ISetting> {
        let id = route.paramMap.get('id');
        return this.settingService.getSingle(+id);
    }
}
