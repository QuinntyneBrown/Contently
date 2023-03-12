// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FieldConfigAggregate.Commands;

public class UpdateFieldConfigRequestValidator : AbstractValidator<UpdateFieldConfigRequest> { }

public class UpdateFieldConfigRequest : IRequest<UpdateFieldConfigResponse>
{

    public Guid FieldConfigId { get; set; }
    public string Key { get; set; }
    public string Type { get; set; }
}

public class UpdateFieldConfigResponse : ResponseBase
{
    public required FieldConfigDto FieldConfig { get; set; }
}


public class UpdateFieldConfigRequestHandler : IRequestHandler<UpdateFieldConfigRequest, UpdateFieldConfigResponse>
{
    private readonly ILogger<UpdateFieldConfigRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public UpdateFieldConfigRequestHandler(ILogger<UpdateFieldConfigRequestHandler> logger, IContentServiceDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UpdateFieldConfigResponse> Handle(UpdateFieldConfigRequest request, CancellationToken cancellationToken)
    {
        var fieldConfig = await _context.FieldConfigs.SingleAsync(x => x.FieldConfigId == request.FieldConfigId);

        fieldConfig.Key = request.Key;

        fieldConfig.Type = request.Type;

        await _context.SaveChangesAsync(cancellationToken);

        return new()
        {
            FieldConfig = fieldConfig.ToDto()
        };

    }

}



