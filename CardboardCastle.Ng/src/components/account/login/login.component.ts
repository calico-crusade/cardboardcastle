import { Component } from '@angular/core';
import {
    HelperService,
    ApiService
} from './../../../services';

@Component({
    selector: 'login-comp',
    templateUrl: './login.template.html',
    styleUrls: ['./login.style.scss']
})
export class Login {
    email: string = '';
    password: string = '';

    loading: boolean = false;

    errors: string[] = [];

    constructor(
        private hlp: HelperService,
        private api: ApiService
    ) { }

    login() {
        this.errors = [];
        this.loading = false;

        if (!this.hlp.validateEmail(this.email)) {
            this.errors.push('Invalid email address');
        }

        if (this.password.length < 4) {
            this.errors.push('Invalid Password');
        }

        if (this.errors.length > 0)
            return;

        this.loading = true;
        this.api.login(this.email, this.password).subscribe(t => {
            this.errors = t;
            this.loading = false;
        });
    }
}