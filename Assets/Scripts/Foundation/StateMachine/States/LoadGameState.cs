using DefaultNamespace;
using DefaultNamespace.Services;
using DefaultNamespace.Services.Factory;
using DefaultNamespace.UI;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameState: IPayloadState<GameData>
{
    private readonly IFactory _factory;
    private readonly INetworkRunner _networkRunner;

    public LoadGameState(IFactory factory, INetworkRunner networkRunner)
    {
        _factory           = factory;
        _networkRunner = networkRunner;
    }

    public void Enter(GameData payload)
    {
        Debug.Log("Loading...");
        _networkRunner.StartGame(payload.GameMode, payload.LobbyName);
    }

    public void Enter()
    {
    }

    public void Exit()
    {
        
    }
}