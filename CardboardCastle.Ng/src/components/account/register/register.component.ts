import { Component } from '@angular/core';
import { NetworkService, HelperService } from './../../../services';

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
        private net: NetworkService,
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

        this.net.post('account/register', {
            emailAddress: this.email,
            firstName: this.firstname,
            lastName: this.lastname,
            password: this.password
        }).subscribe(t => {
            console.log('Success', t);
            this.loading = false;
        }, t => {
            console.log(`Error occurred:`, t);
            this.loading = false;
                var statusCode: number = t.status;
                if (statusCode === 400) {
                    this.errors.push('Please ensure everything is filled out correctly.');
                } else if (statusCode === 409) {
                    this.errors.push('Email address already linked to an account.');
                } else {
                    this.errors.push('Something went wrong, please contact an administrator.');
                }
        });
    }
}