using DefaultNamespace.Services;
using DefaultNamespace.Services.Factory;
using DefaultNamespace.Services.InputService;

public class Game 
{
    private readonly MainRunner _runner;
    private GameStateMachine _stateMachine;

    public Game(MainRunner runner)
    {
        _runner       = runner;
        _stateMachine = new GameStateMachine(RegisterServices());
    }
    
    private Services RegisterServices()
    {
        var services = Services.Container;
        services
            .Add<IInputService>(new InputService())
            .Add<IAssetProvider>(new AssetProvider())
            .Add<INetworkRunner>(_runner)
            .Add<IFactory>(new GameFactory(_runner.Runner, services.Get<IAssetProvider>()));

        _runner.Core.Init(services.Get<IFactory>(), services.Get<IInputService>());
        
        return services;
    }

    public void Start()
    {
        _stateMachine.Enter<InitLobbyState>();
    }
}