import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild, Router } from '@angular/router';
import { ApiService } from './../api.service';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild {

    constructor(
        private api: ApiService,
        private rtr: Router
    ) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        var sub = new Subject<boolean>();

        var ob = this.api.user();

        if (!ob) {
            this.rtr.navigate(['/account/login']);
            return false;
        }

        ob.subscribe(t => {
            sub.next(true);
        }, t => {
            this.rtr.navigate(['/account/login']);
            sub.next(false);
        });

        return sub.asObservable();
    }

    canActivateChild(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.canActivate(next, state);
    }
}