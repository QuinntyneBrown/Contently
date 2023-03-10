// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using ContentService.Core.AggregateModel.FormConfigAggregate.Commands;
using ContentService.Core.AggregateModel.FormConfigAggregate.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace ContentService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class FormConfigController
{
    private readonly IMediator _mediator;

    private readonly ILogger<FormConfigController> _logger;

    public FormConfigController(IMediator mediator, ILogger<FormConfigController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [SwaggerOperation(
        Summary = "Update FormConfig",
        Description = @"Update FormConfig"
    )]
    [HttpPut(Name = "updateFormConfig")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(UpdateFormConfigResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UpdateFormConfigResponse>> Update([FromBody] UpdateFormConfigRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Create FormConfig",
        Description = @"Create FormConfig"
    )]
    [HttpPost(Name = "createFormConfig")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(CreateFormConfigResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CreateFormConfigResponse>> Create([FromBody] CreateFormConfigRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get FormConfigs",
        Description = @"Get FormConfigs"
    )]
    [HttpGet(Name = "getFormConfigs")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetFormConfigsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetFormConfigsResponse>> Get(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFormConfigsRequest(), cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get FormConfig by id",
        Description = @"Get FormConfig by id"
    )]
    [HttpGet("{formConfigId:guid}", Name = "getFormConfigById")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetFormConfigByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetFormConfigByIdResponse>> GetById([FromRoute] Guid formConfigId, CancellationToken cancellationToken)
    {
        var request = new GetFormConfigByIdRequest() { FormConfigId = formConfigId };

        var response = await _mediator.Send(request, cancellationToken);

        if (response.FormConfig == null)
        {
            return new NotFoundObjectResult(request.FormConfigId);
        }

        return response;
    }

    [SwaggerOperation(
        Summary = "Delete FormConfig",
        Description = @"Delete FormConfig"
    )]
    [HttpDelete("{formConfigId:guid}", Name = "deleteFormConfig")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(DeleteFormConfigResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DeleteFormConfigResponse>> Delete([FromRoute] Guid formConfigId, CancellationToken cancellationToken)
    {
        var request = new DeleteFormConfigRequest() { FormConfigId = formConfigId };

        return await _mediator.Send(request, cancellationToken);
    }

}


