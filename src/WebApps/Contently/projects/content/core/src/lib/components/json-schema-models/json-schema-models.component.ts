// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JsonSchemaModelDetailComponent } from '../json-schema-model-detail';
import { JsonSchemaModelListComponent } from '../json-schema-model-list';
import { JsonSchemaModel, JsonSchemaModelStore } from '../../models';
import { ListDetailDirective } from 'list-detail';
import { map } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'cms-json-schema-models',
  standalone: true,
  imports: [
    CommonModule, 
    JsonSchemaModelDetailComponent,
    JsonSchemaModelListComponent,
    ListDetailDirective
  ],
  templateUrl: './json-schema-models.component.html',
  styleUrls: ['./json-schema-models.component.scss']
})
export class JsonSchemaModelsComponent implements OnInit {

  public jsonSchemaModelStore = inject(JsonSchemaModelStore);

  private readonly _router = inject(Router);

  ngOnInit(): void {
    this.jsonSchemaModelStore.load();
  }

  public vm$ = this.jsonSchemaModelStore.state$;

  public handleSelect(jsonSchemaModel: any) {

    console.log(jsonSchemaModel);
    
    if(jsonSchemaModel.jsonSchemaModelId) {
      this._router.navigate(["/","json-schema-models","edit", jsonSchemaModel.jsonSchemaModelId]);
    } else {
      this._router.navigate(["/","json-schema-models","create"]);
    }
  }
}

