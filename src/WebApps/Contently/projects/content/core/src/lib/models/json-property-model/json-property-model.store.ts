// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { inject, Injectable } from "@angular/core";
import { ComponentStore, tapResponse } from "@ngrx/component-store";
import { exhaustMap, map, noop, tap, withLatestFrom } from "rxjs";
import { JsonPropertyModel } from "./json-property-model";
import { JsonPropertyModelService } from "./json-property-model.service";

export interface JsonPropertyModelState {
    jsonPropertyModels: JsonPropertyModel[]
}

const initialJsonPropertyModelState = {
    jsonPropertyModels: []
};

@Injectable({
    providedIn:"root"
})
export class JsonPropertyModelStore extends ComponentStore<JsonPropertyModelState> {
    private  readonly _jsonPropertyModelService = inject(JsonPropertyModelService);

    constructor() {
        super(initialJsonPropertyModelState);        
    }

    readonly save = (jsonPropertyModel:JsonPropertyModel, nextFn: {(response:any): void} | null = null, errorFn: {(response:any): void} | null = null) => {        
        
        const apiRequest$ = jsonPropertyModel.jsonPropertyModelId ? this._jsonPropertyModelService.update({ jsonPropertyModel }) : this._jsonPropertyModelService.create({ jsonPropertyModel });
        
        const updateFn = jsonPropertyModel?.jsonPropertyModelId ? ([response, jsonPropertyModels]: [any, JsonPropertyModel[]]) => this.patchState({

            jsonPropertyModels: jsonPropertyModels.map(t => response.jsonPropertyModel.jsonPropertyModelId == t.jsonPropertyModelId ? response.jsonPropertyModel : t)
        })
        :(([response, jsonPropertyModels]: [any, JsonPropertyModel[]]) => this.patchState({ jsonPropertyModels: [...jsonPropertyModels, response.jsonPropertyModel ]}));
        
        return this.effect<void>(
            exhaustMap(()=> apiRequest$.pipe(
                withLatestFrom(this.select(x => x.jsonPropertyModels)),
                tap(updateFn),
                tapResponse(
                    nextFn || noop,
                    errorFn || noop
                )
            )
        ))();
    }

    readonly delete = this.effect<JsonPropertyModel>(
        exhaustMap((jsonPropertyModel) => this._jsonPropertyModelService.delete({ jsonPropertyModel: jsonPropertyModel }).pipe( 
            withLatestFrom(this.select(x => x.jsonPropertyModels )),           
            tapResponse(
                ([_, jsonPropertyModels]) => this.patchState({ jsonPropertyModels: jsonPropertyModels.filter(t => t.jsonPropertyModelId != jsonPropertyModel.jsonPropertyModelId )}),
                noop
            )
        ))
    );

    readonly load = this.effect<void>(
        exhaustMap(_ => this._jsonPropertyModelService.get().pipe(            
            tapResponse(
                jsonPropertyModels => this.patchState({ jsonPropertyModels }),
                noop                
            )
        ))
    );    
}
