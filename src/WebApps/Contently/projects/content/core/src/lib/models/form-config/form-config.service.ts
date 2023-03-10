// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../constants';
import { map, Observable } from 'rxjs';
import { FormConfig } from './form-config';

@Injectable({
  providedIn: 'root'
})
export class FormConfigService {

  constructor(
    @Inject(BASE_URL) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  public get(): Observable<Array<FormConfig>> {
    return this._client.get<{ formConfigs: Array<FormConfig> }>(`${this._baseUrl}api/1.0/formConfig`)
      .pipe(
        map(x => x.formConfigs)
      );
  }

  public getById(options: { formConfigId: string }): Observable<FormConfig> {
    return this._client.get<{ formConfig: FormConfig }>(`${this._baseUrl}api/1.0/formConfig/${options.formConfigId}`)
      .pipe(
        map(x => x.formConfig)
      );
  }

  public delete(options: { formConfig: FormConfig }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/1.0/formConfig/${options.formConfig.formConfigId}`);
  }

  public create(options: { formConfig: FormConfig }): Observable<{ formConfigId: string  }> {    
    return this._client.post<{ formConfigId: string }>(`${this._baseUrl}api/1.0/formConfig`, { formConfig: options.formConfig });
  }

  public update(options: { formConfig: FormConfig }): Observable<{ formConfigId: string }> {    
    return this._client.post<{ formConfigId: string }>(`${this._baseUrl}api/1.0/formConfig`, { formConfig: options.formConfig });
  }
}
