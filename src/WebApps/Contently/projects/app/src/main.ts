// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { importProvidersFrom } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { RouterModule } from '@angular/router';


bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(
      RouterModule.forRoot([
        { path: '', redirectTo: 'landing', pathMatch: 'full'},
        { path: 'login', loadComponent: () => import('./app/login/login.component').then(m => m.LoginComponent) },
        { path: 'landing', loadComponent: () => import('./app/landing/landing.component').then(m => m.LandingComponent) },
        { path: 'content', loadComponent: () => import('./app/content/content.component').then(m => m.ContentComponent) }
      ]),     
    )
  ]
}).catch((err) => console.error(err));
