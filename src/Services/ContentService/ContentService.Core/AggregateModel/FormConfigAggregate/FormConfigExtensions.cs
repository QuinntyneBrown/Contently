// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FormConfigAggregate;

public static class FormConfigExtensions
{
    public static FormConfigDto ToDto(this FormConfig formConfig)
    {
        return new FormConfigDto
        {
            Name = formConfig.Name,
            Fields = formConfig.Fields.Select(x => x.ToDto()).ToList(),
            FormConfigId = formConfig.FormConfigId,
        };

    }

    public async static Task<List<FormConfigDto>> ToDtosAsync(this IQueryable<FormConfig> formConfigs, CancellationToken cancellationToken)
    {
        return await formConfigs.Select(x => x.ToDto()).ToListAsync(cancellationToken);
    }

}


