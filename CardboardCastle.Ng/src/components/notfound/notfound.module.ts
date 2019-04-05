﻿import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NotFound } from './notfound.component';
import { ServiceModule } from './../../services';

const ROUTES: Routes = [
    {
        path: '',
        component: NotFound
    }
]

const COMP = [
    NotFound
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ServiceModule,
        RouterModule.forChild(ROUTES)
    ],
    declarations: [
        ...COMP
    ],
    exports: [
        ...COMP
    ]
})
export class NotFoundModule { }