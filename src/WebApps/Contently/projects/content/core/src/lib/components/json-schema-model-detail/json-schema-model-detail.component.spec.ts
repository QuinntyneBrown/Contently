import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JsonSchemaModelDetailComponent } from './json-schema-model-detail.component';

describe('JsonSchemaModelDetailComponent', () => {
  let component: JsonSchemaModelDetailComponent;
  let fixture: ComponentFixture<JsonSchemaModelDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ JsonSchemaModelDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JsonSchemaModelDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
