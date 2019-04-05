import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { UtilityCalculator } from './utility-calculator.component';
import { EditUtilityComponent } from './edit-utility/edit-utility.component';
import { ServiceModule } from './../../services';

const ROUTES: Routes = [
    {
        path: '',
        component: UtilityCalculator
    }
]

const COMP = [
    UtilityCalculator,
    EditUtilityComponent
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ServiceModule,
        RouterModule.forChild(ROUTES),
        NgbModule
    ],
    declarations: [
        ...COMP
    ],
    exports: [
        ...COMP
    ],
    entryComponents: [
        ...COMP
    ]
})
export class UtilityCalculatorModule { }