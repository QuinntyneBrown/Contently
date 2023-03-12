// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FormConfigAggregate;

public class FormConfig
{
    public FormConfig()
    {
        Fields = new List<FieldConfig>();
    }
    public Guid FormConfigId { get; set; }
    public string Name { get; set; }
    public List<FieldConfig> Fields { get; set; }

}
