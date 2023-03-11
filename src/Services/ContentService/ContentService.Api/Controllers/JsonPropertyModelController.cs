// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using ContentService.Core.AggregateModel.JsonPropertyModelAggregate.Commands;
using ContentService.Core.AggregateModel.JsonPropertyModelAggregate.Queries;
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
public class JsonPropertyModelController
{
    private readonly IMediator _mediator;

    private readonly ILogger<JsonPropertyModelController> _logger;

    public JsonPropertyModelController(IMediator mediator,ILogger<JsonPropertyModelController> logger){
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [SwaggerOperation(
        Summary = "Update JsonPropertyModel",
        Description = @"Update JsonPropertyModel"
    )]
    [HttpPut(Name = "updateJsonPropertyModel")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(UpdateJsonPropertyModelResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UpdateJsonPropertyModelResponse>> Update([FromBody]UpdateJsonPropertyModelRequest  request,CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Create JsonPropertyModel",
        Description = @"Create JsonPropertyModel"
    )]
    [HttpPost(Name = "createJsonPropertyModel")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(CreateJsonPropertyModelResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CreateJsonPropertyModelResponse>> Create([FromBody]CreateJsonPropertyModelRequest  request,CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get JsonPropertyModels",
        Description = @"Get JsonPropertyModels"
    )]
    [HttpGet(Name = "getJsonPropertyModels")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetJsonPropertyModelsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetJsonPropertyModelsResponse>> Get(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetJsonPropertyModelsRequest(), cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get JsonPropertyModel by id",
        Description = @"Get JsonPropertyModel by id"
    )]
    [HttpGet("{jsonPropertyModelId:guid}", Name = "getJsonPropertyModelById")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetJsonPropertyModelByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetJsonPropertyModelByIdResponse>> GetById([FromRoute]Guid jsonPropertyModelId,CancellationToken cancellationToken)
    {
        var request = new GetJsonPropertyModelByIdRequest(){JsonPropertyModelId = jsonPropertyModelId};

        var response = await _mediator.Send(request, cancellationToken);

        if (response.JsonPropertyModel == null)
        {
            return new NotFoundObjectResult(request.JsonPropertyModelId);
        }

        return response;
    }

    [SwaggerOperation(
        Summary = "Delete JsonPropertyModel",
        Description = @"Delete JsonPropertyModel"
    )]
    [HttpDelete("{jsonPropertyModelId:guid}", Name = "deleteJsonPropertyModel")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(DeleteJsonPropertyModelResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DeleteJsonPropertyModelResponse>> Delete([FromRoute]Guid jsonPropertyModelId,CancellationToken cancellationToken)
    {
        var request = new DeleteJsonPropertyModelRequest() {JsonPropertyModelId = jsonPropertyModelId };

        return await _mediator.Send(request, cancellationToken);
    }

}


