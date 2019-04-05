import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';

// Import routing module
import { AppRoutingModule } from './app.routing';

// Import 3rd party components
import { LocalStorageModule } from 'angular-2-local-storage';

// Import local modules
import { ServiceModule } from './../services';

@NgModule({
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule.forRoot(),
        LocalStorageModule.forRoot({
            prefix: 'cardboardcastle',
            storageType: 'localStorage'
        }),
        ServiceModule
    ],
    declarations: [
        AppComponent
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
