// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FormConfigAggregate.Queries;

public class GetFormConfigsRequest : IRequest<GetFormConfigsResponse> { }

public class GetFormConfigsResponse : ResponseBase
{
    public required List<FormConfigDto> FormConfigs { get; set; }
}


public class GetFormConfigsRequestHandler : IRequestHandler<GetFormConfigsRequest, GetFormConfigsResponse>
{
    private readonly ILogger<GetFormConfigsRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public GetFormConfigsRequestHandler(ILogger<GetFormConfigsRequestHandler> logger, IContentServiceDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetFormConfigsResponse> Handle(GetFormConfigsRequest request, CancellationToken cancellationToken)
    {
        return new()
        {
            FormConfigs = await _context.FormConfigs.AsNoTracking().ToDtosAsync(cancellationToken)
        };

    }

}



