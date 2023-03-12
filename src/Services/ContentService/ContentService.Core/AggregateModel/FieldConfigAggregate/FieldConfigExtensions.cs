// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FieldConfigAggregate;

public static class FieldConfigExtensions
{
    public static PropsDto ToDto(this Props props)
    {
        return new PropsDto
        {
            Label = props.Label,
            Placeholder = props.Placeholder,
            Required = props.Required
        };
    }

    public static FieldConfigDto ToDto(this FieldConfig fieldConfig)
    {
        return new FieldConfigDto
        {
            Key = fieldConfig.Key,
            Type = fieldConfig.Type,
            FieldConfigId = fieldConfig.FieldConfigId,
            Props = fieldConfig.Props.ToDto()
        };

    }

    public async static Task<List<FieldConfigDto>> ToDtosAsync(this IQueryable<FieldConfig> fieldConfigs, CancellationToken cancellationToken)
    {
        return await fieldConfigs.Select(x => x.ToDto()).ToListAsync(cancellationToken);
    }

}


