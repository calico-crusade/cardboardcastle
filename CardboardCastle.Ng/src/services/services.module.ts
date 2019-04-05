import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { HelperService } from './helper.service';
import { NetworkService } from './network.service';
import { StorageService } from './storage.service';

const SERV = [
    HelperService,
    NetworkService,
    StorageService
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule
    ],
    exports: [],
    providers: [
        ...SERV
    ]
})
export class ServiceModule { }

