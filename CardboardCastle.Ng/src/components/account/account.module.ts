import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { Register } from './register/register.component';
import { EditAccount } from './edit/edit-account.component';
import { Login } from './login/login.component';

const ROUTES: Routes = [
    {
        path: 'register',
        component: Register
    },
    {
        path: 'login',
        component: Login
    },
    {
        path: '',
        component: EditAccount 
    }
]

const COMP = [
    Register,
    Login,
    EditAccount
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
export class AccountModule { }