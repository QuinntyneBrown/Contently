// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.FieldConfigAggregate.Queries;

public class GetFieldConfigsPageRequest: IRequest<GetFieldConfigsPageResponse>
{
    public required int PageSize { get; set; }
    public required int Index { get; set; }
}


public class GetFieldConfigsPageResponse: ResponseBase
{
    public required int Length { get; set; }
    public required List<FieldConfigDto> Entities  { get; set; }
}


public class GetFieldConfigsPageRequestHandler: IRequestHandler<GetFieldConfigsPageRequest,GetFieldConfigsPageResponse>
{
    private readonly ILogger<GetFieldConfigsPageRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public GetFieldConfigsPageRequestHandler(ILogger<GetFieldConfigsPageRequestHandler> logger,IContentServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetFieldConfigsPageResponse> Handle(GetFieldConfigsPageRequest request,CancellationToken cancellationToken)
    {
        var query = from fieldConfig in _context.FieldConfigs
            select fieldConfig;

        var length = await _context.FieldConfigs.AsNoTracking().CountAsync();

        var fieldConfigs = await query.Page(request.Index, request.PageSize).AsNoTracking()
            .Select(x => x.ToDto()).ToListAsync();

        return new ()
        {
            Length = length,
            Entities = fieldConfigs
        };

    }

}



