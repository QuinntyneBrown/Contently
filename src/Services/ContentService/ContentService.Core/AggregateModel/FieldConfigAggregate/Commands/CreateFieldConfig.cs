// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FieldConfigAggregate.Commands;

public class CreateFieldConfigRequestValidator : AbstractValidator<CreateFieldConfigRequest> { }

public class CreateFieldConfigRequest : IRequest<CreateFieldConfigResponse>
{

    public string Key { get; set; }
    public string Type { get; set; }
}

public class CreateFieldConfigResponse : ResponseBase
{
    public required FieldConfigDto FieldConfig { get; set; }
}


public class CreateFieldConfigRequestHandler : IRequestHandler<CreateFieldConfigRequest, CreateFieldConfigResponse>
{
    private readonly ILogger<CreateFieldConfigRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public CreateFieldConfigRequestHandler(ILogger<CreateFieldConfigRequestHandler> logger, IContentServiceDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateFieldConfigResponse> Handle(CreateFieldConfigRequest request, CancellationToken cancellationToken)
    {
        var fieldConfig = new FieldConfig();

        _context.FieldConfigs.Add(fieldConfig);

        fieldConfig.Key = request.Key;
        fieldConfig.Type = request.Type;

        await _context.SaveChangesAsync(cancellationToken);

        return new()
        {
            FieldConfig = fieldConfig.ToDto()
        };

    }

}



