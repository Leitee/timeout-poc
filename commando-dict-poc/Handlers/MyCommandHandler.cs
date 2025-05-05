using MyTimeoutApp.Models;
using MyTimeoutApp.Services;

namespace MyTimeoutApp.Handlers;

public class MyCommandHandler
{
    private readonly TimeoutService _timeoutService;

    public MyCommandHandler(TimeoutService timeoutService)
    {
        _timeoutService = timeoutService;
    }

    public Task Handle(MyCommand command)
    {
        var correlationId = _timeoutService.StartTimeout(
            onTimeout: id =>
            {
                Console.WriteLine($"Timeout reached for ID: {id}");
                // Aquí podrías invocar lógica adicional (e.g. enviar alerta, modificar estado)
            },
            timeout: TimeSpan.FromSeconds(20)
        );

        Console.WriteLine($"Started timeout operation for ID: {correlationId}");

        // Simulación: la respuesta externa podría llegar después
        // En ese caso se debe llamar a: _timeoutService.Complete(correlationId, "ok");

        return Task.CompletedTask;
    }
} 