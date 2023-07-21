using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using DefaultNamespace.Services.Factory;
using DefaultNamespace.Services.InputService;

[RequireComponent(typeof(NetworkRunner))]
public class NetworkCore : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private MainRunner _mainRunner;

    private IInputService _inputService;

    public void Init(IFactory factory, IInputService inputService)
    {
        _inputService = inputService;
        _mainRunner.Init(factory);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        _mainRunner.JoinPlayer(runner, player);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        _mainRunner.Left(runner, player);
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (runner.ActivePlayers.Count() == 1)
        {
            return;
        }
        Debug.Log($"{runner.ActivePlayers.Count()}");

        var data = _inputService.GetNetworkInput();

        if (Input.GetKey(KeyCode.W))
            data.Direction += Vector3.up;

        if (Input.GetKey(KeyCode.S))
            data.Direction += Vector3.down;

        if (Input.GetKey(KeyCode.A))
            data.Direction += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            data.Direction += Vector3.right;

        if (Input.GetKey(KeyCode.Space))
            data.IsFireUp = true;

        data.Direction = data.Direction.normalized;

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

    public void OnConnectedToServer(NetworkRunner runner) { }

    public void OnDisconnectedFromServer(NetworkRunner runner) { }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
        byte[] token) { }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }
}