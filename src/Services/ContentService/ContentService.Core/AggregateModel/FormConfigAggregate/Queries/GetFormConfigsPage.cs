// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FormConfigAggregate.Queries;

public class GetFormConfigsPageRequest : IRequest<GetFormConfigsPageResponse>
{
    public required int PageSize { get; set; }
    public required int Index { get; set; }
}


public class GetFormConfigsPageResponse : ResponseBase
{
    public required int Length { get; set; }
    public required List<FormConfigDto> Entities { get; set; }
}


public class GetFormConfigsPageRequestHandler : IRequestHandler<GetFormConfigsPageRequest, GetFormConfigsPageResponse>
{
    private readonly ILogger<GetFormConfigsPageRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public GetFormConfigsPageRequestHandler(ILogger<GetFormConfigsPageRequestHandler> logger, IContentServiceDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetFormConfigsPageResponse> Handle(GetFormConfigsPageRequest request, CancellationToken cancellationToken)
    {
        var query = from formConfig in _context.FormConfigs
                    select formConfig;

        var length = await _context.FormConfigs.AsNoTracking().CountAsync();

        var formConfigs = await query.Page(request.Index, request.PageSize).AsNoTracking()
            .Select(x => x.ToDto()).ToListAsync();

        return new()
        {
            Length = length,
            Entities = formConfigs
        };

    }

}



