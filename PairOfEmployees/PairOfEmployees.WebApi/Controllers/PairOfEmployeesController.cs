using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PairOfEmployees.Application.PairOfEmployeesData.Commands;
using PairOfEmployees.Application.PairOfEmployeesData.Queries;
using static PairOfEmployees.Application.PairOfEmployeesData.Commands.LoadCsvDataCommand;


namespace PairOfEmployees.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PairOfEmployeesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMediator _mediator;

        public PairOfEmployeesController(IWebHostEnvironment env, IMediator mediator)
        {
            _env = env;
            _mediator = mediator;
        }

        [HttpGet("files")]
        public ActionResult<IEnumerable<string>> GetFiles()
        {
            var folder = Path.Combine(_env.ContentRootPath, "UploadedFiles");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            var files = Directory.GetFiles(folder, "*.csv").Select(Path.GetFileName);
            return Ok(files)
        }

        [HttpPost("upload")]
        public async Task<ActionResult<int>> Upload([FromQuery] string file, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(file))
                return BadRequest("File name must be provided.");

            var folder = Path.Combine(_env.ContentRootPath, "UploadedFiles");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var fullPath = Path.Combine(folder, file);
            if (!System.IO.File.Exists(fullPath))
                return NotFound($"File '{file}' not found on the server.");

            var count = await _mediator.Send(new LoadCsvDataCommand(fullPath), ct);
            return Ok(count);
        }

        [HttpGet("preview")]
        public async Task<ActionResult> Preview(CancellationToken cancellation)
        {
           
            var rows = await _mediator.Send(new GetDataPreviewQuery(), cancellation);
            return Ok(rows);
        }

        [HttpGet("longest-pair")]
        public async Task<ActionResult> GetLongestPair(CancellationToken cancellation)
        {
            var result = await _mediator.Send(new GetLongestPairOnCommonProjectQuery(), cancellation);
            if (result is null) return NotFound("No pairs found.");
            return Ok(result);
        }

    }
}
