import { Component, OnInit } from '@angular/core';
import { ApiService, IUser, StorageService } from './../services';
import { Router } from '@angular/router';

@Component({
    selector: 'body',
    templateUrl: './app.component.html',
    styleUrls: [ './app.style.scss' ]
})
export class AppComponent implements OnInit {

    isNavbarCollapsed = true;
    user: IUser = null;

    constructor(
        private api: ApiService,
        private str: StorageService,
        private rtr: Router
    ) { }

    ngOnInit() {
        this.triggerUserTest();
        this.str.idChanged.subscribe(t => this.triggerUserTest());
    }

    signout() {
        this.api.logout();
        this.rtr.navigate(['/account/login']);
    }

    private triggerUserTest() {
        var ob = this.api.user();
        if (!ob) {
            this.user = null;
            return;
        }

        ob.subscribe(t => {
            this.user = t;
        });
    }
}
