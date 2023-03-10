// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using ContentService.Core.AggregateModel.FieldConfigAggregate.Commands;
using ContentService.Core.AggregateModel.FieldConfigAggregate.Queries;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;

namespace ContentService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class FieldConfigController
{
    private readonly IMediator _mediator;

    private readonly ILogger<FieldConfigController> _logger;

    public FieldConfigController(IMediator mediator,ILogger<FieldConfigController> logger){
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [SwaggerOperation(
        Summary = "Update FieldConfig",
        Description = @"Update FieldConfig"
    )]
    [HttpPut(Name = "updateFieldConfig")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(UpdateFieldConfigResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UpdateFieldConfigResponse>> Update([FromBody]UpdateFieldConfigRequest  request,CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Create FieldConfig",
        Description = @"Create FieldConfig"
    )]
    [HttpPost(Name = "createFieldConfig")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(CreateFieldConfigResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CreateFieldConfigResponse>> Create([FromBody]CreateFieldConfigRequest  request,CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get FieldConfigs",
        Description = @"Get FieldConfigs"
    )]
    [HttpGet(Name = "getFieldConfigs")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetFieldConfigsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetFieldConfigsResponse>> Get(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFieldConfigsRequest(), cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get FieldConfig by id",
        Description = @"Get FieldConfig by id"
    )]
    [HttpGet("{fieldConfigId:guid}", Name = "getFieldConfigById")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetFieldConfigByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetFieldConfigByIdResponse>> GetById([FromRoute]Guid fieldConfigId,CancellationToken cancellationToken)
    {
        var request = new GetFieldConfigByIdRequest(){FieldConfigId = fieldConfigId};

        var response = await _mediator.Send(request, cancellationToken);

        if (response.FieldConfig == null)
        {
            return new NotFoundObjectResult(request.FieldConfigId);
        }

        return response;
    }

    [SwaggerOperation(
        Summary = "Delete FieldConfig",
        Description = @"Delete FieldConfig"
    )]
    [HttpDelete("{fieldConfigId:guid}", Name = "deleteFieldConfig")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(DeleteFieldConfigResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DeleteFieldConfigResponse>> Delete([FromRoute]Guid fieldConfigId,CancellationToken cancellationToken)
    {
        var request = new DeleteFieldConfigRequest() {FieldConfigId = fieldConfigId };

        return await _mediator.Send(request, cancellationToken);
    }

}


