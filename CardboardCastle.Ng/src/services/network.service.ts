import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface IAnalysis {
    name: string;
    route: string;
    parameters: string;
}

@Injectable()
export class NetworkService {

    private base = 'api/';

    constructor(
        private net: HttpClient
    ) { }

    public get<T>(url: string) {
        return this.net.get<T>(this.handleUrl(url));
    }

    public post<T>(url: string, data: any) {
        return this.net.post<T>(this.handleUrl(url), data);
    }

    public delete<T>(url: string) {
        return this.net.delete<T>(this.handleUrl(url));
    }

    public put<T>(url: string, data: any) {
        return this.net.put<T>(this.handleUrl(url), data);
    }

    private handleUrl(url: string) {
        if (url.indexOf('://') !== -1)
            return url;

        return this.base + url;
    }
}