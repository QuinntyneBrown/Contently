// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FieldConfigAggregate;

public class FieldConfig
{
    public FieldConfig()
    {
        Props = new Props();
    }

    public required Guid FieldConfigId { get; set; }
    public required string Key { get; set; }
    public required string Type { get; set; }
    public required Props Props { get; set; }
}

