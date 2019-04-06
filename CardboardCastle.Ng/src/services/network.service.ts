import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { Router } from "@angular/router";
import { StorageService } from "./storage.service";

export interface IAnalysis {
    name: string;
    route: string;
    parameters: string;
}

@Injectable()
export class NetworkService {

    private base = 'api/';
    private errors: string[] = [];
    private debug: boolean = true;
    private get authToken(): string {
        return this.storage.token;
    }

    constructor(
        private http: Http,
        private router: Router,
        private storage: StorageService
    ) {
        this.errors = [];
    }

    get<T>(url: string, doContentType: boolean = true): Observable<T> {
        let options = this.doHeaders(doContentType);
        return this.doSub(this.http.get(this.doUrl(url), options));
    }
    post<T>(url: string, data: any, doContentType: boolean = true): Observable<T> {
        return this.basePut<T>(url, data, doContentType);
    }
    put<T>(url: string, data: any, doContentType: boolean = true): Observable<T> {
        let options = this.doHeaders(doContentType);
        return this.doSub(this.http.put(this.doUrl(url), data, options));
    }
    patch<T>(url: string, data: any, doContentType: boolean = true): Observable<T> {
        let options = this.doHeaders(doContentType);
        return this.doSub(this.http.patch(this.doUrl(url), options, data));
    }

    private doSub<T>(obs: Observable<Response>) {
        var ob1: Observable<T> = obs.map(this.extractData)
            .catch(this.handleError);

        return ob1;
    }

    private extractData(res: Response) {
        try {
            let body = res.json();
            return body || [];
        }
        catch (Exception) {
            return {
                message: res.text()
            }
        }
    }

    private handleError(error: Response | any) {
        if (error instanceof Response &&
            error.status == 401 &&
            this.router) {
            this.router.navigate(['/login']);
            return null;
        }

        let errMsg: string;
        let body: any;
        if (error instanceof Response) {
            body = error.json() || '';
            errMsg = `${error.status} - ${error.statusText || ''}`;
        } else {
            body = {};
            errMsg = error.message ? error.message : error.toString();
        }
        if (this.debug) {
            console.error('' + errMsg + '');
        }
        return Observable.throw({
            message: errMsg,
            body: body
        });
    }

    private doUrl(url: string) {
        if (url.indexOf('://') != -1)
            return url;
        return this.base + url;
    }

    private doHeaders(addContentType: boolean) {
        if (!addContentType) {
            if (this.authToken) {
                return new RequestOptions({
                    headers: new Headers({
                        'Authorization': 'Bearer ' + this.authToken
                    })
                });
            }

            return new RequestOptions();
        }

        if (this.authToken) {
            let headers = new Headers(
                {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.authToken
                }
            );
            return new RequestOptions({ headers: headers });
        }

        let headers = new Headers(
            {
                'Content-Type': 'application/json'
            }
        );
        return new RequestOptions({ headers: headers });
    }

    private basePut<T>(url: string, data: any, addContentType: boolean = true): Observable<T> {
        let options = this.doHeaders(addContentType);
        return this.doSub(this.http.post(this.doUrl(url), data, options));
    }
}