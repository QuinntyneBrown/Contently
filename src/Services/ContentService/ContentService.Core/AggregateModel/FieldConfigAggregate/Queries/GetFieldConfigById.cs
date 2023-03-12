// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FieldConfigAggregate.Queries;

public class GetFieldConfigByIdRequest : IRequest<GetFieldConfigByIdResponse>
{
    public required Guid FieldConfigId { get; set; }
}


public class GetFieldConfigByIdResponse : ResponseBase
{
    public required FieldConfigDto FieldConfig { get; set; }
}


public class GetFieldConfigByIdRequestHandler : IRequestHandler<GetFieldConfigByIdRequest, GetFieldConfigByIdResponse>
{
    private readonly ILogger<GetFieldConfigByIdRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public GetFieldConfigByIdRequestHandler(ILogger<GetFieldConfigByIdRequestHandler> logger, IContentServiceDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetFieldConfigByIdResponse> Handle(GetFieldConfigByIdRequest request, CancellationToken cancellationToken)
    {
        return new()
        {
            FieldConfig = (await _context.FieldConfigs.AsNoTracking().SingleOrDefaultAsync(x => x.FieldConfigId == request.FieldConfigId)).ToDto()
        };

    }

}



