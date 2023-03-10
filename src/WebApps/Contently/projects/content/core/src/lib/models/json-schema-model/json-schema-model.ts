// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { JsonPropertyModel } from "../json-property-model";

export type JsonSchemaModel = {
  jsonSchemaModelId?: string;
  properties?: JsonPropertyModel[];
};