// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FormConfigAggregate.Commands;

public class DeleteFormConfigRequestValidator: AbstractValidator<DeleteFormConfigRequest> { }

public class DeleteFormConfigRequest: IRequest<DeleteFormConfigResponse>
{
    public required Guid FormConfigId { get; set; }
}


public class DeleteFormConfigResponse: ResponseBase
{
    public required FormConfigDto FormConfig { get; set; }
}


public class DeleteFormConfigRequestHandler: IRequestHandler<DeleteFormConfigRequest,DeleteFormConfigResponse>
{
    private readonly ILogger<DeleteFormConfigRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public DeleteFormConfigRequestHandler(ILogger<DeleteFormConfigRequestHandler> logger,IContentServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<DeleteFormConfigResponse> Handle(DeleteFormConfigRequest request,CancellationToken cancellationToken)
    {
        var formConfig = await _context.FormConfigs.FindAsync(request.FormConfigId);

        _context.FormConfigs.Remove(formConfig);

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            FormConfig = formConfig.ToDto()
        };
    }

}



