import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { Register } from './register/register.component';
import { EditAccount } from './edit/edit-account.component';
import { ServiceModule } from './../../services';

const ROUTES: Routes = [
    {
        path: 'register',
        component: Register
    },
    {
        path: '',
        component: EditAccount 
    }
]

const COMP = [
    Register,
    EditAccount
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
export class AccountModule { }