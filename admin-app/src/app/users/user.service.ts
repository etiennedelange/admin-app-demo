import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { catchError, retry, finalize } from "rxjs/operators";
import { of, BehaviorSubject, Observable } from "rxjs";
import { DataSource } from "@angular/cdk/table";
import { CollectionViewer } from "@angular/cdk/collections";
import { IResetPassword } from '../shared/services/authentication.service';
import { UserRole } from '../shared/user-role';
import { Repository } from "../common/repository-base";

@Injectable({
    providedIn: "root"
})
export class UserService extends Repository<IUser> {

    constructor(public http: HttpClient) {
        super(http, 'users')
    }

    getUsername(id: number): Observable<IResetPassword> {
        return this.http
            .get<IResetPassword>(`api/users/getusername/${id}`);
    }

    activate(id: string, token: string): Observable<Object> {
        return this.http.post("api/users/activate", null, {
            params: new HttpParams()
                .set('id', id)
                .set('token', token)
        });
    }
}

export interface IUser {
    id: number;
    fullName: string;
    userName: string;
    isActive: boolean;
    roles?: UserRole[];
}

export class UsersDataSource extends DataSource<IUser> {
    private usersSubject: BehaviorSubject<IUser[]>;;
    private loadingSubject: BehaviorSubject<boolean>;
    loading$: Observable<boolean>;

    constructor(
        private userService: UserService,
        initialValues?: IUser[]
    ) {
        super();
        this.usersSubject = new BehaviorSubject(initialValues);

        this.loadingSubject = new BehaviorSubject(false);
        this.loading$ = this.loadingSubject.asObservable()
    }

    connect(collectionViewer: CollectionViewer): Observable<IUser[] | readonly IUser[]> {
        return this.usersSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.usersSubject.complete();
    }

    load(pageIndex: number, pageSize: number, searchText: string, sortOrder: string = "asc"): void {
        this.loadingSubject.next(true);
        this.userService
            .get(pageIndex, pageSize, searchText, sortOrder)
            .pipe(
                retry(3),
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(res => this.usersSubject.next(res));
    }
}

export interface IUserDialogData {
    id: number;
}