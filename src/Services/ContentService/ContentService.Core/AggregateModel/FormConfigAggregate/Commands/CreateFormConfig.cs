// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FormConfigAggregate.Commands;

public class CreateFormConfigRequestValidator: AbstractValidator<CreateFormConfigRequest> { }

public class CreateFormConfigRequest: IRequest<CreateFormConfigResponse> {
    public string Name { get; set; }
    public List<FieldConfigDto> Fields { get; set; }
}

public class CreateFormConfigResponse: ResponseBase
{
    public required FormConfigDto FormConfig { get; set; }
}


public class CreateFormConfigRequestHandler: IRequestHandler<CreateFormConfigRequest,CreateFormConfigResponse>
{
    private readonly ILogger<CreateFormConfigRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public CreateFormConfigRequestHandler(ILogger<CreateFormConfigRequestHandler> logger,IContentServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateFormConfigResponse> Handle(CreateFormConfigRequest request,CancellationToken cancellationToken)
    {
        var formConfig = new FormConfig()
        {
            Name = request.Name
        };

        _context.FormConfigs.Add(formConfig);

        formConfig.Name = request.Name;

        foreach(var field in request.Fields)
        {

        }

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            FormConfig = formConfig.ToDto()
        };

    }

}



