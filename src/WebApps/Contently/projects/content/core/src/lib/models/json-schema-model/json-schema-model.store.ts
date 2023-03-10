// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { inject, Injectable } from "@angular/core";
import { ComponentStore, tapResponse } from "@ngrx/component-store";
import { exhaustMap, map, noop, tap, withLatestFrom } from "rxjs";
import { JsonSchemaModel } from "./json-schema-model";
import { JsonSchemaModelService } from "./json-schema-model.service";

export interface JsonSchemaModelState {
    jsonSchemaModels: JsonSchemaModel[]
}

const initialJsonSchemaModelState = {
    jsonSchemaModels: []
};

@Injectable({
    providedIn:"root"
})
export class JsonSchemaModelStore extends ComponentStore<JsonSchemaModelState> {
    private  readonly _jsonSchemaModelService = inject(JsonSchemaModelService);

    constructor() {
        super(initialJsonSchemaModelState);        
    }

    readonly save = (jsonSchemaModel:JsonSchemaModel, nextFn: {(response:any): void} | null = null, errorFn: {(response:any): void} | null = null) => {        
        
        const apiRequest$ = jsonSchemaModel.jsonSchemaModelId ? this._jsonSchemaModelService.update({ jsonSchemaModel }) : this._jsonSchemaModelService.create({ jsonSchemaModel });
        
        const updateFn = jsonSchemaModel?.jsonSchemaModelId ? ([response, jsonSchemaModels]: [any, JsonSchemaModel[]]) => this.patchState({

            jsonSchemaModels: jsonSchemaModels.map(t => response.jsonSchemaModel.jsonSchemaModelId == t.jsonSchemaModelId ? response.jsonSchemaModel : t)
        })
        :(([response, jsonSchemaModels]: [any, JsonSchemaModel[]]) => this.patchState({ jsonSchemaModels: [...jsonSchemaModels, response.jsonSchemaModel ]}));
        
        return this.effect<void>(
            exhaustMap(()=> apiRequest$.pipe(
                withLatestFrom(this.select(x => x.jsonSchemaModels)),
                tap(updateFn),
                tapResponse(
                    nextFn || noop,
                    errorFn || noop
                )
            )
        ))();
    }

    readonly delete = this.effect<JsonSchemaModel>(
        exhaustMap((jsonSchemaModel) => this._jsonSchemaModelService.delete({ jsonSchemaModel: jsonSchemaModel }).pipe( 
            withLatestFrom(this.select(x => x.jsonSchemaModels )),           
            tapResponse(
                ([_, jsonSchemaModels]) => this.patchState({ jsonSchemaModels: jsonSchemaModels.filter(t => t.jsonSchemaModelId != jsonSchemaModel.jsonSchemaModelId )}),
                noop
            )
        ))
    );

    readonly load = this.effect<void>(
        exhaustMap(_ => this._jsonSchemaModelService.get().pipe(            
            tapResponse(
                jsonSchemaModels => this.patchState({ jsonSchemaModels }),
                noop                
            )
        ))
    );    
}
