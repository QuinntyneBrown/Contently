// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { createJsonSchemaModelsViewModel } from './create-json-schema-models-view-model';
import { PushModule } from '@ngrx/component';
import { JsonSchemaModelsComponent } from '@content/core';

@Component({
  selector: 'app-json-schema-models',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, PushModule, JsonSchemaModelsComponent],
  templateUrl: './json-schema-models.component.html',
  styleUrls: ['./json-schema-models.component.scss']
})
export class JsonSchemaModelsPageComponent {

  public vm$ = createJsonSchemaModelsViewModel();
}
