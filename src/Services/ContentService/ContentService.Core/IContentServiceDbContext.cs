// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using ContentService.Core.AggregateModel.FieldConfigAggregate;
using ContentService.Core.AggregateModel.FormConfigAggregate;
using ContentService.Core.AggregateModel.UserAggregate;

namespace ContentService.Core;

public interface IContentServiceDbContext
{
    DbSet<Content> Contents { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<JsonSchemaModel> JsonSchemaModels { get; set; }
    DbSet<JsonPropertyModel> JsonPropertyModels { get; set; }
    DbSet<FieldConfig> FieldConfigs {  get; set; }
    DbSet<FormConfig> FormConfigs { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}


