using HomeWard.Application.WorkSessions;
using HomeWard.Desktop.Infrastructure.Client.WorkSessionApiClient;
using HomeWard.Desktop.Infrastructure.Services.UserService;
using HomeWard.Desktop.Infrastructure.Services.WorkSessionTimer;
using HomeWard.Domain.Entities;
using HomeWard.Domain.Enums;

namespace HomeWard.Desktop.Infrastructure.Services.WorkSessionSync;

public class WorkSessionSyncService : IWorkSessionSyncService
{
    private readonly IWorkSessionTimer _timer;
    private readonly IWorkSessionApiClient _apiClient;
    private readonly IUserService _userService;

    public Guid? ActiveSessionId { get; private set; }

    public WorkSessionSyncService(
        IWorkSessionTimer timer,
        IWorkSessionApiClient apiClient,
        IUserService userService)
    {
        _timer = timer;
        _apiClient = apiClient;
        _userService = userService;
    }

    public void Start()
    {
        _timer.TransitionAdded += OnTransitionAdded;
        _timer.StateChanged += OnStateChanged;
    }

    public void Stop()
    {
        _timer.TransitionAdded -= OnTransitionAdded;
        _timer.StateChanged -= OnStateChanged;
    }

    private async void OnStateChanged()
    {
        if (_userService.User is null) return;

        var userId = _userService.User.Id;

        // Inicia a sessão na API quando o timer começa
        if (_timer.State == SessionState.Working && ActiveSessionId is null)
        {
            try
            {
                var session = await _apiClient.StartAsync(userId);
                ActiveSessionId = session.Id;

                //Adiciona a transição inicial (NotStarted → Working) na API
                var request = new AddTransitionRequest(
                    ActiveSessionId.Value,
                    SessionState.NotStarted,
                    SessionState.Working,
                    DateTime.UtcNow,
                    new TimeSpan(0));

                await _apiClient.AddTransitionAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WorkSessionSync] Erro ao iniciar sessão: {ex.Message}");
            }
        }

        // Finaliza a sessão na API quando o timer para
        if (_timer.State == SessionState.Finished && ActiveSessionId is not null)
        {
            try
            {
                var request = new FinishWorkSessionRequest(
                    ActiveSessionId.Value,
                    _timer.BillableTime,
                    _timer.WorkedTime,
                    _timer.RestedTime,
                    _timer.PausedTime);

                await _apiClient.FinishAsync(request);
                ActiveSessionId = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WorkSessionSync] Erro ao finalizar sessão: {ex.Message}");
            }
        }
    }

    private async void OnTransitionAdded(SessionTransition transition)
    {
        if (ActiveSessionId is null) return;

        // Não envia a transição inicial (NotStarted → Working), pois StartAsync já cria a sessão
        if (transition.FromState == SessionState.NotStarted) return;

        try
        {
            var request = new AddTransitionRequest(
                ActiveSessionId.Value,
                transition.FromState,
                transition.ToState,
                transition.Timestamp,
                transition.TimeElapsed);

            await _apiClient.AddTransitionAsync(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WorkSessionSync] Erro ao adicionar transição: {ex.Message}");
        }
    }
}