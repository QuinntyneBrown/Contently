// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace ContentService.Core.AggregateModel.UserAggregate.Commands;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest> { }

public class CreateUserRequest : IRequest<CreateUserResponse>
{
    public string Username { get; set; }
    public Guid UserId { get; set; }
}


public class CreateUserResponse : ResponseBase
{
    public UserDto User { get; set; }
}


public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly ILogger<CreateUserRequestHandler> _logger;

    private readonly IContentServiceDbContext _context;

    public CreateUserRequestHandler(ILogger<CreateUserRequestHandler> logger, IContentServiceDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User(request.Username);

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return new()
        {
            User = user.ToDto()
        };

    }

}



