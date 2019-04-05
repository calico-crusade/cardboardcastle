import { Component, Input, OnInit, ElementRef, ViewChild } from '@angular/core';
import { HelperService } from './../../services';
@Component({
    selector: 'dashboard-comp',
    templateUrl: 'dashboard.template.html',
    styleUrls: [ './dashboard.style.scss' ]
})
export class Dashboard implements OnInit {

    
    constructor(
        private hlp: HelperService
    ) { }

    ngOnInit() {
        
    }
    
}