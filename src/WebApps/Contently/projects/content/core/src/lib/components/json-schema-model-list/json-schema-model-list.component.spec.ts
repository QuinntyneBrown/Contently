import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JsonSchemaModelListComponent } from './json-schema-model-list.component';

describe('JsonSchemaModelListComponent', () => {
  let component: JsonSchemaModelListComponent;
  let fixture: ComponentFixture<JsonSchemaModelListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ JsonSchemaModelListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JsonSchemaModelListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
