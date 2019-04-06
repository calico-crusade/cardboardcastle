import { Component, OnInit } from '@angular/core';
import { ApiService, IUser } from './../../../services';

@Component({
    selector: 'edit-account-comp',
    templateUrl: './edit-account.template.html',
    styleUrls: ['./edit-account.style.scss']
})
export class EditAccount implements OnInit {

    user: IUser;
    loading: boolean = true;

    curPass: string;
    newPass: string;

    constructor(
        private api: ApiService
    ) { }

    ngOnInit() {
        this.loading = true;
        this.api.user().subscribe(t => {
            this.user = t;
            this.loading = false;
        });
    }
}