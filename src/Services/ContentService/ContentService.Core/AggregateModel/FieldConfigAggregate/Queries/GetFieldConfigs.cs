// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FieldConfigAggregate.Queries;

public class GetFieldConfigsRequest : IRequest<GetFieldConfigsResponse> { }

public class GetFieldConfigsResponse : ResponseBase
{
    public required List<FieldConfigDto> FieldConfigs { get; set; }
}


public class GetFieldConfigsRequestHandler : IRequestHandler<GetFieldConfigsRequest, GetFieldConfigsResponse>
{
    private readonly ILogger<GetFieldConfigsRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public GetFieldConfigsRequestHandler(ILogger<GetFieldConfigsRequestHandler> logger, IContentServiceDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetFieldConfigsResponse> Handle(GetFieldConfigsRequest request, CancellationToken cancellationToken)
    {
        return new()
        {
            FieldConfigs = await _context.FieldConfigs.AsNoTracking().ToDtosAsync(cancellationToken)
        };

    }

}



