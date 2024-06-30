using Microsoft.AspNetCore.Mvc;
using Narya.Sms.Core.Interfaces;
using Narya.Sms.Core.Models;
using TestSuite.Models;

namespace TestSuite.Controllers;

[ApiController]
[Route("[controller]")]
public class SmsController : ControllerBase
{
    private readonly ILogger<SmsController> _logger;
    private readonly ISmsService _smsService;

    public SmsController(ILogger<SmsController> logger, ISmsService smsService)
    {
        _logger = logger;
        _smsService = smsService;
    }

    [HttpPost("twilio")]
    public async Task<IActionResult> SendUsingTwilio([FromBody] SmsModel options)
    {
        var smsResult = SmsOptions.Create(options.Message, options.To.ToArray());
        if (smsResult.IsFailure) return BadRequest(smsResult.Errors);
        var result = await _smsService.Send(smsResult.Value);
        if (result.IsFailure) return BadRequest(result.Errors);
        return Ok();
    }
}