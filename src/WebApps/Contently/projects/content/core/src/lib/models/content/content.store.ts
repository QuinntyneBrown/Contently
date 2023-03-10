// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { inject, Injectable } from "@angular/core";
import { ComponentStore, tapResponse } from "@ngrx/component-store";
import { exhaustMap, map, noop, tap, withLatestFrom } from "rxjs";
import { Content } from "./content";
import { ContentService } from "./content.service";

export interface ContentState {
    contents: Content[]
}

const initialContentState = {
    contents: []
};

@Injectable({
    providedIn:"root"
})
export class ContentStore extends ComponentStore<ContentState> {
    private  readonly _contentService = inject(ContentService);

    constructor() {
        super(initialContentState);        
    }

    readonly save = (content:Content, nextFn: {(response:any): void} | null = null, errorFn: {(response:any): void} | null = null) => {        
        
        const apiRequest$ = content.contentId ? this._contentService.update({ content }) : this._contentService.create({ content });
        
        const updateFn = content?.contentId ? ([response, contents]: [any, Content[]]) => this.patchState({

            contents: contents.map(t => response.content.contentId == t.contentId ? response.content : t)
        })
        :(([response, contents]: [any, Content[]]) => this.patchState({ contents: [...contents, response.content ]}));
        
        return this.effect<void>(
            exhaustMap(()=> apiRequest$.pipe(
                withLatestFrom(this.select(x => x.contents)),
                tap(updateFn),
                tapResponse(
                    nextFn || noop,
                    errorFn || noop
                )
            )
        ))();
    }

    readonly delete = this.effect<Content>(
        exhaustMap((content) => this._contentService.delete({ content: content }).pipe( 
            withLatestFrom(this.select(x => x.contents )),           
            tapResponse(
                ([_, contents]) => this.patchState({ contents: contents.filter(t => t.contentId != content.contentId )}),
                noop
            )
        ))
    );

    readonly load = this.effect<void>(
        exhaustMap(_ => this._contentService.get().pipe(            
            tapResponse(
                contents => this.patchState({ contents }),
                noop                
            )
        ))
    );    
}
