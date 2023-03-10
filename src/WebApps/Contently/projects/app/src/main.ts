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
    { provide: "PAGE_SIZE_OPTIONS", useValue: ["5","10","25"] },
    importProvidersFrom(
      HttpClientModule,
      RouterModule.forRoot([
        { path: '', redirectTo: 'json-schema-models', pathMatch: 'full'},
        { path: 'login', loadComponent: () => import('./app/login-page/login-page.component').then(m => m.LoginPageComponent) },
        { path: 'landing', loadComponent: () => import('./app/landing-page/landing-page.component').then(m => m.LandingPageComponent) },
        { path: 'content', loadComponent: () => import('./app/content-page/content-page.component').then(m => m.ContentPageComponent) },
        { path: 'json-schema-models', loadComponent: () => import('./app/json-schema-models-page/json-schema-models-page.component').then(m => m.JsonSchemaModelsPageComponent) },
        { path: 'json-schema-models/create', loadComponent: () => import('./app/json-schema-models-page/json-schema-models-page.component').then(m => m.JsonSchemaModelsPageComponent) },
        { path: 'json-schema-models/edit/:jsonSchemaModelId', loadComponent: () => import('./app/json-schema-models-page/json-schema-models-page.component').then(m => m.JsonSchemaModelsPageComponent) }
      ]), BrowserAnimationsModule,     
    )
  ]
}).catch((err) => console.error(err));
