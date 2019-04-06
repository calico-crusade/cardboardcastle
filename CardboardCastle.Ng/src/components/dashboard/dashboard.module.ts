import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { Dashboard } from './dashboard.component';

const ROUTES: Routes = [
    {
        path: '',
        component: Dashboard
    }
]

const COMP = [
    Dashboard
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forChild(ROUTES)
    ],
    declarations: [
        ...COMP
    ],
    exports: [
        ...COMP
    ]
})
export class DashboardModule { }