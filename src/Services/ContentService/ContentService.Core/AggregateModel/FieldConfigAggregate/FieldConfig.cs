// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FieldConfigAggregate;

public class FieldConfig
{
    public FieldConfig()
    {
        Props = new Props();
    }

    public Guid FieldConfigId { get; set; }
    public string Key { get; set; }
    public string Type { get; set; }
    public Props Props { get; set; }
}