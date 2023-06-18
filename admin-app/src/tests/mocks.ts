import { Observable, of } from "rxjs";

export class SnackBarServiceMock {
    open() {
        return {};
    }
}

export class FormBuilderMock {
    group() {
        return {
            disable() {

            },
            get() {
                return {
                    get valueChanges(): Observable<any> {
                        return of("");
                    }
                };
            }
        };
    }
}

export class AuthenticationServiceMock {
    hasSomeRoles(): boolean {
        return true;
    }

    public hasAllRoles(): boolean {
        return true;
    }
}

export class NgControlMock {
    valueAccessor: {}
}