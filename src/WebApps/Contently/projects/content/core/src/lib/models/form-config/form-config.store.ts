// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { inject, Injectable } from "@angular/core";
import { ComponentStore, tapResponse } from "@ngrx/component-store";
import { exhaustMap, map, noop, tap, withLatestFrom } from "rxjs";
import { FormConfig } from "./form-config";
import { FormConfigService } from "./form-config.service";

export interface FormConfigState {
    formConfigs: FormConfig[]
}

const initialFormConfigState = {
    formConfigs: []
};

@Injectable({
    providedIn:"root"
})
export class FormConfigStore extends ComponentStore<FormConfigState> {
    private  readonly _formConfigService = inject(FormConfigService);

    constructor() {
        super(initialFormConfigState);        
    }

    readonly save = (formConfig:FormConfig, nextFn: {(response:any): void} | null = null, errorFn: {(response:any): void} | null = null) => {        
        
        const apiRequest$ = formConfig.formConfigId ? this._formConfigService.update({ formConfig }) : this._formConfigService.create({ formConfig });
        
        const updateFn = formConfig?.formConfigId ? ([response, formConfigs]: [any, FormConfig[]]) => this.patchState({

            formConfigs: formConfigs.map(t => response.formConfig.formConfigId == t.formConfigId ? response.formConfig : t)
        })
        :(([response, formConfigs]: [any, FormConfig[]]) => this.patchState({ formConfigs: [...formConfigs, response.formConfig ]}));
        
        return this.effect<void>(
            exhaustMap(()=> apiRequest$.pipe(
                withLatestFrom(this.select(x => x.formConfigs)),
                tap(updateFn),
                tapResponse(
                    nextFn || noop,
                    errorFn || noop
                )
            )
        ))();
    }

    readonly delete = this.effect<FormConfig>(
        exhaustMap((formConfig) => this._formConfigService.delete({ formConfig: formConfig }).pipe( 
            withLatestFrom(this.select(x => x.formConfigs )),           
            tapResponse(
                ([_, formConfigs]) => this.patchState({ formConfigs: formConfigs.filter(t => t.formConfigId != formConfig.formConfigId )}),
                noop
            )
        ))
    );

    readonly load = this.effect<void>(
        exhaustMap(_ => this._formConfigService.get().pipe(            
            tapResponse(
                formConfigs => this.patchState({ formConfigs }),
                noop                
            )
        ))
    );    
}
