using System;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using DefaultNamespace.Services;
using DefaultNamespace.Services.Factory;
using DefaultNamespace.Services.InputService;

[RequireComponent(typeof(NetworkRunner))]
public class NetworkCore : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private MainRunner _mainRunner;

    private IFactory _factory;
    private IInputService _inputService;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public void Init(IFactory factory, IInputService inputService)
    {
        _factory      = factory;
        _inputService = inputService;
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Joined");
      
            var networkPlayerObject = InitPlayer(runner, player);
            _spawnedCharacters.Add(player, networkPlayerObject);
        
    }

    private NetworkObject InitPlayer(NetworkRunner runner, PlayerRef player)
    {
        Vector3 spawnPosition =
            new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
        var networkPlayerObject =
            _factory.CreateNetObject<NetworkObject>(AssetPathes.Network.PlayerPref, spawnPosition, player);
        return networkPlayerObject;
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // Find and remove the players avatar
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
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