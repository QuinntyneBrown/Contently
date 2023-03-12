// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.JsonSchemaModelAggregate.Commands;

public class CreateJsonSchemaModelRequestValidator : AbstractValidator<CreateJsonSchemaModelRequest>
{
    public CreateJsonSchemaModelRequestValidator()
    {

        RuleFor(x => x.Name).NotNull();

    }

}


public class CreateJsonSchemaModelRequest : IRequest<CreateJsonSchemaModelResponse>
{
    public string Name { get; set; }
}


public class CreateJsonSchemaModelResponse
{
    public required JsonSchemaModelDto JsonSchemaModel { get; set; }
}


public class CreateJsonSchemaModelRequestHandler : IRequestHandler<CreateJsonSchemaModelRequest, CreateJsonSchemaModelResponse>
{
    private readonly IContentServiceDbContext _context;

    private readonly ILogger<CreateJsonSchemaModelRequestHandler> _logger;

    public CreateJsonSchemaModelRequestHandler(ILogger<CreateJsonSchemaModelRequestHandler> logger, IContentServiceDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateJsonSchemaModelResponse> Handle(CreateJsonSchemaModelRequest request, CancellationToken cancellationToken)
    {
        var jsonSchemaModel = new JsonSchemaModel();

        _context.JsonSchemaModels.Add(jsonSchemaModel);

        jsonSchemaModel.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return new()
        {
            JsonSchemaModel = jsonSchemaModel.ToDto()
        };

    }

}



