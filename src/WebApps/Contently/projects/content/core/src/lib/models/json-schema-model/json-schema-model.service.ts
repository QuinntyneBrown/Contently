// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../constants';
import { map, Observable } from 'rxjs';
import { JsonSchemaModel } from './json-schema-model';

@Injectable({
  providedIn: 'root'
})
export class JsonSchemaModelService {

  constructor(
    @Inject(BASE_URL) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  public get(): Observable<Array<JsonSchemaModel>> {
    return this._client.get<{ jsonSchemaModels: Array<JsonSchemaModel> }>(`${this._baseUrl}api/1.0/jsonschemamodel`)
      .pipe(
        map(x => x.jsonSchemaModels)
      );
  }

  public getById(options: { jsonSchemaModelId: string }): Observable<JsonSchemaModel> {
    return this._client.get<{ jsonSchemaModel: JsonSchemaModel }>(`${this._baseUrl}api/1.0/jsonschemamodel/${options.jsonSchemaModelId}`)
      .pipe(
        map(x => x.jsonSchemaModel)
      );
  }

  public delete(options: { jsonSchemaModel: JsonSchemaModel }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/1.0/jsonschemamodel/${options.jsonSchemaModel.jsonSchemaModelId}`);
  }

  public create(options: { jsonSchemaModel: JsonSchemaModel }): Observable<{ jsonSchemaModelId: string  }> {    
    return this._client.post<{ jsonSchemaModelId: string }>(`${this._baseUrl}api/1.0/jsonschemamodel`, { jsonSchemaModel: options.jsonSchemaModel });
  }

  public update(options: { jsonSchemaModel: JsonSchemaModel }): Observable<{ jsonSchemaModelId: string }> {    
    return this._client.post<{ jsonSchemaModelId: string }>(`${this._baseUrl}api/1.0/jsonschemamodel`, { jsonSchemaModel: options.jsonSchemaModel });
  }
}
