// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FieldConfigAggregate.Commands;

public class DeleteFieldConfigRequestValidator: AbstractValidator<DeleteFieldConfigRequest> { }

public class DeleteFieldConfigRequest: IRequest<DeleteFieldConfigResponse>
{
    public required Guid FieldConfigId { get; set; }
}


public class DeleteFieldConfigResponse: ResponseBase
{
    public required FieldConfigDto FieldConfig { get; set; }
}


public class DeleteFieldConfigRequestHandler: IRequestHandler<DeleteFieldConfigRequest,DeleteFieldConfigResponse>
{
    private readonly ILogger<DeleteFieldConfigRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public DeleteFieldConfigRequestHandler(ILogger<DeleteFieldConfigRequestHandler> logger,IContentServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<DeleteFieldConfigResponse> Handle(DeleteFieldConfigRequest request,CancellationToken cancellationToken)
    {
        var fieldConfig = await _context.FieldConfigs.FindAsync(request.FieldConfigId);

        _context.FieldConfigs.Remove(fieldConfig);

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            FieldConfig = fieldConfig.ToDto()
        };
    }

}



