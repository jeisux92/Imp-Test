import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader';
import { AppComponent } from './app.component';
import { AppModule } from './app.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
    imports: [
        AppModule, 
        ServerModule, 
        ModuleMapLoaderModule,
        SharedModule
    ],
    bootstrap: [AppComponent]
})
export class AppServerModule { }
