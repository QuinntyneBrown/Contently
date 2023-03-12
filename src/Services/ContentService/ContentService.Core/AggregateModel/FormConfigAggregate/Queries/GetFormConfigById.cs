// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FormConfigAggregate.Queries;

public class GetFormConfigByIdRequest : IRequest<GetFormConfigByIdResponse>
{
    public required Guid FormConfigId { get; set; }
}


public class GetFormConfigByIdResponse : ResponseBase
{
    public required FormConfigDto FormConfig { get; set; }
}


public class GetFormConfigByIdRequestHandler : IRequestHandler<GetFormConfigByIdRequest, GetFormConfigByIdResponse>
{
    private readonly ILogger<GetFormConfigByIdRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public GetFormConfigByIdRequestHandler(ILogger<GetFormConfigByIdRequestHandler> logger, IContentServiceDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetFormConfigByIdResponse> Handle(GetFormConfigByIdRequest request, CancellationToken cancellationToken)
    {
        return new()
        {
            FormConfig = (await _context.FormConfigs.AsNoTracking().SingleOrDefaultAsync(x => x.FormConfigId == request.FormConfigId)).ToDto()
        };

    }

}



