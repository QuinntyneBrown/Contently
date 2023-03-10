// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../constants';
import { map, Observable } from 'rxjs';
import { JsonPropertyModel } from './json-property-model';

@Injectable({
  providedIn: 'root'
})
export class JsonPropertyModelService {

  constructor(
    @Inject(BASE_URL) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  public get(): Observable<Array<JsonPropertyModel>> {
    return this._client.get<{ jsonPropertyModels: Array<JsonPropertyModel> }>(`${this._baseUrl}api/1.0/jsonPropertyModel`)
      .pipe(
        map(x => x.jsonPropertyModels)
      );
  }

  public getById(options: { jsonPropertyModelId: string }): Observable<JsonPropertyModel> {
    return this._client.get<{ jsonPropertyModel: JsonPropertyModel }>(`${this._baseUrl}api/1.0/jsonPropertyModel/${options.jsonPropertyModelId}`)
      .pipe(
        map(x => x.jsonPropertyModel)
      );
  }

  public delete(options: { jsonPropertyModel: JsonPropertyModel }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/1.0/jsonPropertyModel/${options.jsonPropertyModel.jsonPropertyModelId}`);
  }

  public create(options: { jsonPropertyModel: JsonPropertyModel }): Observable<{ jsonPropertyModelId: string  }> {    
    return this._client.post<{ jsonPropertyModelId: string }>(`${this._baseUrl}api/1.0/jsonPropertyModel`, { jsonPropertyModel: options.jsonPropertyModel });
  }

  public update(options: { jsonPropertyModel: JsonPropertyModel }): Observable<{ jsonPropertyModelId: string }> {    
    return this._client.post<{ jsonPropertyModelId: string }>(`${this._baseUrl}api/1.0/jsonPropertyModel`, { jsonPropertyModel: options.jsonPropertyModel });
  }
}
