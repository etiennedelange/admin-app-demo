import { Injectable } from '@angular/core';
import {
	HttpRequest,
	HttpHandler,
	HttpEvent,
	HttpInterceptor,
	HttpEventType
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoadingBarService } from './loading-bar.service';
import { tap } from 'rxjs/operators';

@Injectable()
export class LoadingBarInterceptor implements HttpInterceptor {

	constructor(private loadingBarService: LoadingBarService) { }

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		this.showLoadingBar();

		return next.handle(request)
			.pipe(
				tap((event: HttpEvent<any>) => {
					if (event.type == HttpEventType.Response) {
						this.hideLoadingBar();
					}
				}, (error) => {
					this.hideLoadingBar();
				}));
	}

	private showLoadingBar() {
		this.loadingBarService.setLoadingState(true);
	}

	private hideLoadingBar() {
		this.loadingBarService.setLoadingState(false);
	}
}
