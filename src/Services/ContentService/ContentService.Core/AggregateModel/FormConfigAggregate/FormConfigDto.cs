// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FormConfigAggregate;

public class FormConfigDto
{
    public FormConfigDto()
    {
        Fields = new List<FieldConfigDto>();
    }
    public required Guid FormConfigId { get; set; }
    public required string Name { get; set; }
    public required List<FieldConfigDto> Fields { get; set; }
}


