import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { NetworkService } from './network.service';
import { StorageService } from './storage.service';
import {
    ILoginResult,
    IUser
} from './models';
import { Subject } from 'rxjs';

@Injectable()
export class ApiService {

    constructor(
        private net: NetworkService,
        private str: StorageService,
        private rtr: Router
    ) { }

    public login(email: string, password: string) {
        var sub = new Subject<string[]>();

        this.net.post<ILoginResult>('account/login', {
            emailAddress: email,
            password: password
        }).subscribe(t => {
            sub.next([]);
            this.str.token = t.token;
            this.str.id = t.user.userId;
            this.rtr.navigate(['/account']);
        }, t => {
            var errors = [];
            var code: number = t.status;
            if (code == 404) {
                errors.push('Invalid email and password combination.');
            } else if (code == 400) {
                errors.push('Please ensure the email and password are filled out!');
            } else {
                errors.push('An error occurred, please contact an administrator');
            }
            sub.next(errors);
        });

        return sub.asObservable();
    }

    public logout() {
        this.str.token = null;
        this.str.id = null;
    }

    public register(email: string, password: string, fname: string, lname: string) {
        var sub = new Subject<string[]>();

        this.net.post('account/register', {
            emailAddress: email,
            firstName: fname,
            lastName: lname,
            password: password
        }).subscribe(t => {
            sub.next([]);
            this.rtr.navigate(['/login']);
        }, t => {
            var errors = [];
            var statusCode: number = t.status;
            if (statusCode === 400) {
                errors.push('Please ensure everything is filled out correctly.');
            } else if (statusCode === 409) {
                errors.push('Email address already linked to an account.');
            } else {
                errors.push('Something went wrong, please contact an administrator.');
            }
            sub.next(errors);
        });

        return sub.asObservable();
    }

    public user(id?: number) {
        if (!this.str.token)
            return null;

        if (id) {
            return this.net.get<IUser>(`account/details/${id}`);
        }

        return this.net.get<IUser>('account/details');
    }
}