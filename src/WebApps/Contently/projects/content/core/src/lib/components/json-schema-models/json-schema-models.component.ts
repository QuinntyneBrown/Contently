// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

// https://github.com/QuinntyneBrown/Blog/blob/6b844a9496427f4717331d1820887b03566e4ead/src/Blog.App/src/app/workspace/contents/contents.component.ts

import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JsonSchemaModelDetailComponent } from '../json-schema-model-detail';
import { JsonSchemaModelListComponent } from '../json-schema-model-list';
import { JsonSchemaModelStore } from '../../models';
import { map } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { combineLatest } from 'rxjs';
import { ListDetailDirective } from '@global/components';

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

  private readonly _activatedRoute = inject(ActivatedRoute);

  ngOnInit(): void {
    this.jsonSchemaModelStore.load();
  }

  public vm$ = combineLatest([
    this.jsonSchemaModelStore.state$,
    this._activatedRoute.paramMap
  ]).pipe(
    map(([state, paramMap]) => {
      return {
        jsonSchemaModels: state.jsonSchemaModels
      }
    })
  );

  public handleSelect(jsonSchemaModel: any) {
    if(jsonSchemaModel.jsonSchemaModelId) {
      this._router.navigate(["/","json-schema-models","edit", jsonSchemaModel.jsonSchemaModelId]);
    } else {
      this._router.navigate(["/","json-schema-models","create"]);
    }
  }
}



