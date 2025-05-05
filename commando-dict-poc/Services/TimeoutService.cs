using System.Collections.Concurrent;

namespace MyTimeoutApp.Services;

public class TimeoutService
{
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<string>> _pending = new();

    public Guid StartTimeout(Action<Guid> onTimeout, TimeSpan timeout)
    {
        var id = Guid.NewGuid();
        var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
        _pending[id] = tcs;

        _ = Task.Run(async () =>
        {
            var completed = await Task.WhenAny(tcs.Task, Task.Delay(timeout));
            if (completed != tcs.Task)
            {
                onTimeout(id);
                _pending.TryRemove(id, out _);
            }
        });

        return id;
    }

    public void Complete(Guid id, string result)
    {
        if (_pending.TryRemove(id, out var tcs))
        {
            tcs.TrySetResult(result);
        }
    }
} 