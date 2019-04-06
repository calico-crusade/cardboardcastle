import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpInterceptor
} from '@angular/common/http';
import { StorageService } from './../storage.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(
        private store: StorageService
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler) {

        if (this.store.token) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${this.store.token}`
                }
            });
        }

        return next.handle(request);
    }
}