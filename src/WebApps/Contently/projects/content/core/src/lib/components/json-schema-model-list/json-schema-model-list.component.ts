// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Component, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventEmitter } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { Inject } from '@angular/core';
import { JsonSchemaModel } from '../../models';
import { Output } from '@angular/core';


@Component({
  selector: 'cms-json-schema-model-list',
  standalone: true,
  imports: [
    CommonModule, 
    MatTableModule, 
    MatPaginatorModule,
    MatIconModule
  ],
  templateUrl: './json-schema-model-list.component.html',
  styleUrls: ['./json-schema-model-list.component.scss']
})
export class JsonSchemaModelListComponent {

  constructor(
    @Inject("PAGE_SIZE_OPTIONS") readonly pageSizeOptions:number[]
  ) {

  }

  private _dataSource!: MatTableDataSource<JsonSchemaModel>;

  @ViewChild(MatPaginator, { static: true }) private _paginator!: MatPaginator;
  
  readonly displayedColumns: string[] = ["name"];

  @Output() public select: EventEmitter<JsonSchemaModel> = new EventEmitter();

  @Input("jsonSchemaModels") set contents(value: JsonSchemaModel[]) {
    this._dataSource = new MatTableDataSource(value);
  }

  get dataSource() { return this._dataSource; }
}


