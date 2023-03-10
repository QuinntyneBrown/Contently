// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../constants';
import { map, Observable } from 'rxjs';
import { FieldConfig } from './field-config';

@Injectable({
  providedIn: 'root'
})
export class FieldConfigService {

  constructor(
    @Inject(BASE_URL) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  public get(): Observable<Array<FieldConfig>> {
    return this._client.get<{ fieldConfigs: Array<FieldConfig> }>(`${this._baseUrl}api/1.0/fieldConfig`)
      .pipe(
        map(x => x.fieldConfigs)
      );
  }

  public getById(options: { fieldConfigId: string }): Observable<FieldConfig> {
    return this._client.get<{ fieldConfig: FieldConfig }>(`${this._baseUrl}api/1.0/fieldConfig/${options.fieldConfigId}`)
      .pipe(
        map(x => x.fieldConfig)
      );
  }

  public delete(options: { fieldConfig: FieldConfig }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/1.0/fieldConfig/${options.fieldConfig.fieldConfigId}`);
  }

  public create(options: { fieldConfig: FieldConfig }): Observable<{ fieldConfigId: string  }> {    
    return this._client.post<{ fieldConfigId: string }>(`${this._baseUrl}api/1.0/fieldConfig`, { fieldConfig: options.fieldConfig });
  }

  public update(options: { fieldConfig: FieldConfig }): Observable<{ fieldConfigId: string }> {    
    return this._client.post<{ fieldConfigId: string }>(`${this._baseUrl}api/1.0/fieldConfig`, { fieldConfig: options.fieldConfig });
  }
}
