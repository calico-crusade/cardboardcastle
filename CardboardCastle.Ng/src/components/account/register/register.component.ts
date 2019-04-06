import { Component } from '@angular/core';
import { HelperService, ApiService } from './../../../services';

@Component({
    selector: 'register-comp',
    templateUrl: './register.template.html',
    styleUrls: [ './register.style.scss' ]
})
export class Register {
    email: string = '';
    password: string = '';
    firstname: string = '';
    lastname: string = '';

    loading: boolean = false;

    errors: string[] = [];

    constructor(
        private api: ApiService,
        private hlp: HelperService
    ) { }


    register() {
        this.errors = [];

        if (!this.hlp.validateEmail(this.email)) {
            this.errors.push('Invalid email address!');
        }

        if (!this.firstname || this.firstname.length <= 1) {
            this.errors.push('Invalid first name!');
        }

        if (!this.lastname || this.lastname.length <= 1) {
            this.errors.push('Invalid last name!');
        }

        if (!this.password || this.password.length < 4) {
            this.errors.push('Invalid password! Needs to be at least 4 characters.');
        }

        if (this.errors.length > 0)
            return;

        this.loading = true;

        this.api.register(this.email, this.password, this.firstname, this.lastname)
            .subscribe(t => {
                this.errors = t;
                this.loading = false;
            });
    }
}