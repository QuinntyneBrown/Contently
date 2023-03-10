// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { importProvidersFrom } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { RouterModule } from '@angular/router';
import { BASE_URL as CONTENT_BASE_URL } from '@content/core';
import { BASE_URL as IDENTITY_BASE_URL } from '@identity/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

bootstrapApplication(AppComponent, {
  providers: [
    { provide: CONTENT_BASE_URL, useValue: 'https://localhost:7161/'},
    importProvidersFrom(
      HttpClientModule,
      RouterModule.forRoot([
        { path: '', redirectTo: 'json-schema-models', pathMatch: 'full'},
        { path: 'login', loadComponent: () => import('./app/login/login.component').then(m => m.LoginComponent) },
        { path: 'landing', loadComponent: () => import('./app/landing/landing.component').then(m => m.LandingComponent) },
        { path: 'content', loadComponent: () => import('./app/content/content.component').then(m => m.ContentComponent) },
        { path: 'json-schema-models', loadComponent: () => import('./app/json-schema-models/json-schema-models.component').then(m => m.JsonSchemaModelsPageComponent) }
      ]), BrowserAnimationsModule,     
    )
  ]
}).catch((err) => console.error(err));
