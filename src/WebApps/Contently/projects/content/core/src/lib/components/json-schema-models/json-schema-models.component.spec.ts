// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JsonSchemaModelsComponent } from './json-schema-models.component';

describe('JsonSchemaModelsComponent', () => {
  let component: JsonSchemaModelsComponent;
  let fixture: ComponentFixture<JsonSchemaModelsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ JsonSchemaModelsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JsonSchemaModelsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

