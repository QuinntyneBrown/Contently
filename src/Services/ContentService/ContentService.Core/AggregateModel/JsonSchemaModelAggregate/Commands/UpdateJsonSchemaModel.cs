// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.JsonSchemaModelAggregate.Commands;

public class UpdateJsonSchemaModelRequestValidator : AbstractValidator<UpdateJsonSchemaModelRequest>
{
    public UpdateJsonSchemaModelRequestValidator()
    {

        RuleFor(x => x.JsonSchemaModelId).NotEqual(default(Guid));
        RuleFor(x => x.Name).NotNull();
    }

}


public class UpdateJsonSchemaModelRequest : IRequest<UpdateJsonSchemaModelResponse>
{
    public Guid JsonSchemaModelId { get; set; }
    public string Name { get; set; }
}


public class UpdateJsonSchemaModelResponse
{
    public required JsonSchemaModelDto JsonSchemaModel { get; set; }
}


public class UpdateJsonSchemaModelRequestHandler : IRequestHandler<UpdateJsonSchemaModelRequest, UpdateJsonSchemaModelResponse>
{
    private readonly IContentServiceDbContext _context;

    private readonly ILogger<UpdateJsonSchemaModelRequestHandler> _logger;

    public UpdateJsonSchemaModelRequestHandler(ILogger<UpdateJsonSchemaModelRequestHandler> logger, IContentServiceDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UpdateJsonSchemaModelResponse> Handle(UpdateJsonSchemaModelRequest request, CancellationToken cancellationToken)
    {
        var jsonSchemaModel = await _context.JsonSchemaModels.SingleAsync(x => x.JsonSchemaModelId == request.JsonSchemaModelId);

        jsonSchemaModel.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return new()
        {
            JsonSchemaModel = jsonSchemaModel.ToDto()
        };

    }

}



