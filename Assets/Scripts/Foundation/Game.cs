using DefaultNamespace;
using DefaultNamespace.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game 
{
    private readonly GameData _gameData;
    private GameStateMachine _stateMachine;

    public Game(GameData gameData)
    {
        _gameData = gameData;
    }

    public void Init()
    {
        var services = RegisterServices();
        _stateMachine = new GameStateMachine(services);
        _stateMachine.Enter<InitServicesState>();
    }

    private Services RegisterServices()
    {
        var services = Services.Container;
        services
            .Add<IAssetProvider>(new AssetProvider());

        return services;
    }

    public void Start()
    {
        SceneManager.LoadScene(Idents.Scenes.Lobby);
       
    }
}