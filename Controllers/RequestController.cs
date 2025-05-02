using Microsoft.AspNetCore.Mvc;
using MyTimeoutApp.Handlers;
using MyTimeoutApp.Models;
using MyTimeoutApp.Services;

namespace MyTimeoutApp.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestController : ControllerBase
{
    private readonly MyCommandHandler _handler;
    private readonly TimeoutService _timeoutService;

    public RequestController(MyCommandHandler handler, TimeoutService timeoutService)
    {
        _handler = handler;
        _timeoutService = timeoutService;
    }

    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody] MyCommand command)
    {
        await _handler.Handle(command);
        return Ok("Command received. Timeout process started.");
    }

    [HttpPost("complete/{id:guid}")]
    public IActionResult Complete(Guid id, [FromBody] string result)
    {
        _timeoutService.Complete(id, result);
        return Ok($"Completed manually: {id}");
    }
} 