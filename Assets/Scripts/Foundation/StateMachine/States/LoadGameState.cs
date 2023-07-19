using DefaultNamespace;
using DefaultNamespace.Services.Factory;
using Fusion;
using UnityEngine;

public class LoadGameState: IPayloadState<GameData>
{
    private readonly IFactory _get;
    private readonly INetworkRunner _networkRunner;

    public LoadGameState(IFactory get, INetworkRunner networkRunner)
    {
        _get           = get;
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