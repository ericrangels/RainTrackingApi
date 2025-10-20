using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RainTrackingApi.Models.DTO;
using RainTrackingApi.Models.Request;
using RainTrackingApi.Models.Response;
using RainTrackingApi.Services.Interfaces;
using RainTrackingApi.Swagger.Examples;
using RainTrackingApi.Validation;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace RainTrackingApi.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class RainLogController : ControllerBase
    {
        private readonly IRainLogService _rainLogService;
        private readonly IMapper _mapper;

        public RainLogController(IRainLogService rainLogService, IMapper mapper)
        {
            _rainLogService = rainLogService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Retrieve rain logs",
            Description = "Gets all rain observation logs for the specified user. Supports optional filtering by rain status."
            )]
        [SwaggerResponse(200, "List of user rain logs", typeof(List<RainLogResponse>))]
        [SwaggerResponse(400, "Validation error", typeof(ValidationErrorResponse))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RainLogResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationErrorResponseExample))]
        public async Task<IActionResult> GetRainLog(
            [FromHeader(Name = "x-userId"), NotEmptyOrWhitespace] string userIdentifier,
            [FromQuery(Name = "rain")] bool? isRaining)
        {
            var rainLogs = await _rainLogService.GetByUserIdAsync(userIdentifier, isRaining);
            var rainLogsDto = _mapper.Map<List<RainLogResponse>>(rainLogs);

            return Ok(rainLogsDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Create rain log",
            Description = "Records a new rain observation log for the specified user. Creates the user if they don't exist."
            )]
        [SwaggerRequestExample(typeof(AddRainLogRequest), typeof(AddRainLogRequestExample))]
        [SwaggerResponse(201, "Created rain log", typeof(RainLogResponse))]
        [SwaggerResponse(400, "Validation error", typeof(ValidationErrorResponse))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(RainLogResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationErrorResponseExample))]
        public async Task<IActionResult> PostRainLog(
            [FromHeader(Name = "x-userId"), NotEmptyOrWhitespace] string userIdentifier,
            [FromBody, Required] AddRainLogRequest rainRequest)
        {
            var createUserLog = _mapper.Map<CreateUserRainLogDto>(rainRequest);
            createUserLog.UserIdentifier = userIdentifier;

            var created = await _rainLogService.CreateAsync(createUserLog);

            var createdDto = _mapper.Map<RainLogResponse>(created);

            return CreatedAtAction(nameof(GetRainLog), null, createdDto);
        }
    }
}
