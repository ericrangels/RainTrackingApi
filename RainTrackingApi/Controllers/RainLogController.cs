using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RainTrackingApi.Models.Domain;
using RainTrackingApi.Models.DTO;
using RainTrackingApi.Services.Interfaces;
using RainTrackingApi.Swagger.Examples;
using RainTrackingApi.Validation;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace RainTrackingApi.Controllers
{
    [ApiController]
    [Route("api/rainlogs")]
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
        [SwaggerOperation(Summary = "Get rain logs for the specified user", Description = "Requires x-userId header")]
        [SwaggerResponse(200, "List of user rain logs", typeof(List<RainLogResponseDto>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RainLogResponseExample))]
        public async Task<IActionResult> GetRainLog(
            [FromHeader(Name = "x-userId"), NotEmptyOrWhitespace] string userIdentifier,
            [FromQuery(Name = "rain")] bool? isRaining)
        {
            var rainLogs = await _rainLogService.GetByUserIdAsync(userIdentifier, isRaining);
            var rainLogsDto = _mapper.Map<List<RainLogResponseDto>>(rainLogs);

            return Ok(rainLogsDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create a new rain log for the specified user", Description = "Requires x-userId header")]
        [SwaggerRequestExample(typeof(AddRainLogRequestDto), typeof(AddRainLogRequestExample))]
        [SwaggerResponse(201, "Created rain log", typeof(RainLogResponseDto))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(RainLogResponseExample))]
        public async Task<IActionResult> PostRainLog(
            [FromHeader(Name = "x-userId"), NotEmptyOrWhitespace] string userIdentifier,
            [FromBody, Required] AddRainLogRequestDto rainRequest)
        {
            var createUserLog = _mapper.Map<CreateUserRainLogModel>(rainRequest);
            createUserLog.UserIdentifier = userIdentifier;

            var created = await _rainLogService.CreateAsync(createUserLog);

            var createdDto = _mapper.Map<RainLogResponseDto>(created);

            return CreatedAtAction(nameof(GetRainLog), null, createdDto);
        }
    }
}
