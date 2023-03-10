import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JsonSchemaModelDetailComponent } from '../json-schema-model-detail';
import { JsonSchemaModelListComponent } from '../json-schema-model-list';
import { JsonSchemaModelStore } from '../../models';

@Component({
  selector: 'cms-json-schema-models',
  standalone: true,
  imports: [
    CommonModule, 
    JsonSchemaModelDetailComponent,
    JsonSchemaModelListComponent
  ],
  templateUrl: './json-schema-models.component.html',
  styleUrls: ['./json-schema-models.component.scss']
})
export class JsonSchemaModelsComponent implements OnInit {

  public jsonSchemaModelStore = inject(JsonSchemaModelStore);

  ngOnInit(): void {
    this.jsonSchemaModelStore.load();
  }
}
