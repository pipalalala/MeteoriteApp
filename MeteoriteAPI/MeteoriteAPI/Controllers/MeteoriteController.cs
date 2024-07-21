using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MeteoriteAPI.Web.Models;
using MeteoriteAPI.Common.Models.Filters;
using MeteoriteAPI.Application.Services.Interfaces;

namespace MeteoriteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeteoriteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMeteoriteService _meteoriteService;
        private readonly IMeteoriteProcessingService _meteoriteProcessingService;

        public MeteoriteController(
            IMapper mapper,
            IMeteoriteService meteoriteService,
            IMeteoriteProcessingService meteoriteProcessingService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _meteoriteService = meteoriteService
                ?? throw new ArgumentNullException(nameof(meteoriteService));
            _meteoriteProcessingService = meteoriteProcessingService;
        }

        // Could be scheduled job
        [HttpPost("processData")]
        public async Task<IActionResult> GetByFilterAsync()
        {
            await _meteoriteProcessingService.PrepareDataAsync();
            await _meteoriteProcessingService.ProcessDataAsync();

            return Ok();
        }

        [HttpPost("getByFilter")]
        [ProducesResponseType<IEnumerable<MeteoriteGroup>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByFilterAsync([FromBody] MeteoriteFilter filter)
        {
            ArgumentNullException.ThrowIfNull(nameof(filter));

            var result = await _meteoriteService.GetByFilterAsync(filter);

            return Ok(_mapper.Map<IEnumerable<MeteoriteGroup>>(result));
        }

        [HttpGet("getYearRangeList")]
        [ProducesResponseType<IEnumerable<int>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetYearRangeListAsync()
        {
            var result = await _meteoriteService.GetYearRangeListAsync();

            return Ok(result);
        }

        [HttpGet("getRecClassList")]
        [ProducesResponseType<IEnumerable<string>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecClassListAsync()
        {
            var result = await _meteoriteService.GetRecClassListAsync();

            return Ok(result);
        }
    }
}
