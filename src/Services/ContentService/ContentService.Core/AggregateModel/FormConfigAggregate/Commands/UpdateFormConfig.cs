// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FormConfigAggregate.Commands;

public class UpdateFormConfigRequestValidator: AbstractValidator<UpdateFormConfigRequest> { }

public class UpdateFormConfigRequest: IRequest<UpdateFormConfigResponse> { }

public class UpdateFormConfigResponse: ResponseBase
{
    public required FormConfigDto FormConfig { get; set; }
}


public class UpdateFormConfigRequestHandler: IRequestHandler<UpdateFormConfigRequest,UpdateFormConfigResponse>
{
    private readonly ILogger<UpdateFormConfigRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public UpdateFormConfigRequestHandler(ILogger<UpdateFormConfigRequestHandler> logger,IContentServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UpdateFormConfigResponse> Handle(UpdateFormConfigRequest request,CancellationToken cancellationToken)
    {
        var formConfig = await _context.FormConfigs.SingleAsync(x => x.FormConfigId == request.FormConfigId);

        formConfig.Name = request.Name;
        formConfig.FieldConfig = request.FieldConfig;
        formConfig.FormConfigId = request.FormConfigId;

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            FormConfig = formConfig.ToDto()
        };

    }

}



