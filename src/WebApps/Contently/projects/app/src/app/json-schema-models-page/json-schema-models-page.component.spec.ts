// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JsonSchemaModelsPageComponent } from './json-schema-models-page.component';

describe('JsonSchemaModelsPageComponent', () => {
  let component: JsonSchemaModelsPageComponent;
  let fixture: ComponentFixture<JsonSchemaModelsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ JsonSchemaModelsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JsonSchemaModelsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

