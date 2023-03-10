// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { inject, Injectable } from "@angular/core";
import { ComponentStore, tapResponse } from "@ngrx/component-store";
import { exhaustMap, map, noop, tap, withLatestFrom } from "rxjs";
import { FieldConfig } from "./field-config";
import { FieldConfigService } from "./field-config.service";

export interface FieldConfigState {
    fieldConfigs: FieldConfig[]
}

const initialFieldConfigState = {
    fieldConfigs: []
};

@Injectable({
    providedIn:"root"
})
export class FieldConfigStore extends ComponentStore<FieldConfigState> {
    private  readonly _fieldConfigService = inject(FieldConfigService);

    constructor() {
        super(initialFieldConfigState);        
    }

    readonly save = (fieldConfig:FieldConfig, nextFn: {(response:any): void} | null = null, errorFn: {(response:any): void} | null = null) => {        
        
        const apiRequest$ = fieldConfig.fieldConfigId ? this._fieldConfigService.update({ fieldConfig }) : this._fieldConfigService.create({ fieldConfig });
        
        const updateFn = fieldConfig?.fieldConfigId ? ([response, fieldConfigs]: [any, FieldConfig[]]) => this.patchState({

            fieldConfigs: fieldConfigs.map(t => response.fieldConfig.fieldConfigId == t.fieldConfigId ? response.fieldConfig : t)
        })
        :(([response, fieldConfigs]: [any, FieldConfig[]]) => this.patchState({ fieldConfigs: [...fieldConfigs, response.fieldConfig ]}));
        
        return this.effect<void>(
            exhaustMap(()=> apiRequest$.pipe(
                withLatestFrom(this.select(x => x.fieldConfigs)),
                tap(updateFn),
                tapResponse(
                    nextFn || noop,
                    errorFn || noop
                )
            )
        ))();
    }

    readonly delete = this.effect<FieldConfig>(
        exhaustMap((fieldConfig) => this._fieldConfigService.delete({ fieldConfig: fieldConfig }).pipe( 
            withLatestFrom(this.select(x => x.fieldConfigs )),           
            tapResponse(
                ([_, fieldConfigs]) => this.patchState({ fieldConfigs: fieldConfigs.filter(t => t.fieldConfigId != fieldConfig.fieldConfigId )}),
                noop
            )
        ))
    );

    readonly load = this.effect<void>(
        exhaustMap(_ => this._fieldConfigService.get().pipe(            
            tapResponse(
                fieldConfigs => this.patchState({ fieldConfigs }),
                noop                
            )
        ))
    );    
}
