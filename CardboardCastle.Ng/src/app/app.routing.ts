import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './../services';

export const routes: Routes = [
    {
        path: '',
        loadChildren: './../components/dashboard/dashboard.module#DashboardModule'
    },
    {
        path: 'utility-calculator',
        loadChildren: './../components/utility-calculator/utility-calculator.module#UtilityCalculatorModule',
        canActivateChild: [AuthGuard]
    },
    {
        path: 'account',
        loadChildren: './../components/account/account.module#AccountModule'
    },
    {
        path: '**',
        loadChildren: './../components/notfound/notfound.module#NotFoundModule'
    }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
