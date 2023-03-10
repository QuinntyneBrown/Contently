// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FieldConfigAggregate;

public class FieldConfigDto
{
    public FieldConfigDto()
    {
        Props = new PropsDto();
    }
    public Guid FieldConfigId { get; set; }
    public string Key { get; set; }
    public string Type { get; set; }    
    public PropsDto Props { get; set; }
}
