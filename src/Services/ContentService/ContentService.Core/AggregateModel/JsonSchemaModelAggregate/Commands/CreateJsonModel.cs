// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.JsonSchemaModelAggregate.Commands;

public class CreateJsonSchemaModelRequestValidator: AbstractValidator<CreateJsonSchemaModelRequest> { }

public class CreateJsonSchemaModelRequest: IRequest<CreateJsonSchemaModelResponse> {

    public CreateJsonSchemaModelRequest()
    {
        Properties = new List<JsonPropertyModelDto>();
    }
    public string Name { get; set; }
    public List<JsonPropertyModelDto> Properties { get; set; }
}

public class CreateJsonSchemaModelResponse: ResponseBase
{
    public required JsonSchemaModelDto JsonSchemaModel { get; set; }
}


public class CreateJsonSchemaModelRequestHandler: IRequestHandler<CreateJsonSchemaModelRequest,CreateJsonSchemaModelResponse>
{
    private readonly ILogger<CreateJsonSchemaModelRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public CreateJsonSchemaModelRequestHandler(ILogger<CreateJsonSchemaModelRequestHandler> logger,IContentServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateJsonSchemaModelResponse> Handle(CreateJsonSchemaModelRequest request,CancellationToken cancellationToken)
    {
        var jsonSchemaModel = new JsonSchemaModel(request.Name);

        _context.JsonSchemaModels.Add(jsonSchemaModel);

        foreach(var prop in request.Properties)
        {
            jsonSchemaModel.Properties.Add(new JsonPropertyModel(prop.Name, prop.Type));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            JsonSchemaModel = jsonSchemaModel.ToDto()
        };

    }

}



