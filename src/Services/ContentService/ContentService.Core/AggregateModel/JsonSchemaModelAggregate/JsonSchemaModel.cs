// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using ContentService.Core.AggregateModel.FormConfigAggregate;

namespace ContentService.Core.AggregateModel.JsonSchemaModelAggregate;

public class JsonSchemaModel
{
    public Guid JsonSchemaModelId { get; set; }
    public Guid FormConfigId { get; set; }
    public Guid? UserId { get; set; }
    public string Name { get; set; }
    public List<JsonPropertyModel> Properties { get; set; }
    public FormConfig FormConfig { get; set; }
    public List<Content> Content { get; set; }
}