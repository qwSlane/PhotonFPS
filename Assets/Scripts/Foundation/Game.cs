using DefaultNamespace.Services;
using DefaultNamespace.Services.Factory;
using UnityEngine;

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
            .Add<IAssetProvider>(new AssetProvider())
            .Add<INetworkRunner>(_runner)
            .Add<IFactory>(new GameFactory(_runner.Runner, services.Get<IAssetProvider>()));

        _runner.Core.Init(services.Get<IFactory>());
        
        return services;
    }

    public void Start()
    {
        _stateMachine.Enter<InitLobbyState>();
    }
}