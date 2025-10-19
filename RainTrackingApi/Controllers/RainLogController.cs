using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RainTrackingApi.Models.Domain;
using RainTrackingApi.Models.DTO;
using RainTrackingApi.Services.Interfaces;
using RainTrackingApi.Validation;
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
        public async Task<IActionResult> GetRainLog([FromHeader(Name = "x-userId"), NotEmptyOrWhitespace] string userIdentifier)
        {
            var rainLogs = await _rainLogService.GetByUserIdAsync(userIdentifier);
            var rainLogsDto = _mapper.Map<List<RainLogResponseDto>>(rainLogs);

            return Ok(rainLogsDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
