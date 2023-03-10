// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { createJsonSchemaModelsPageViewModel } from './create-json-schema-models-page-view-model';
import { PushModule } from '@ngrx/component';
import { JsonSchemaModelsComponent } from '@content/core';

@Component({
  selector: 'app-json-schema-models-page',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, PushModule, JsonSchemaModelsComponent],
  templateUrl: './json-schema-models-page.component.html',
  styleUrls: ['./json-schema-models-page.component.scss']
})
export class JsonSchemaModelsPageComponent {
  public vm$ = createJsonSchemaModelsPageViewModel();
}
