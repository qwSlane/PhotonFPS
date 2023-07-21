using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefaultNamespace;
using DefaultNamespace.Services;
using DefaultNamespace.Services.Factory;
using Fusion;
using UnityEngine;

public class MainRunner : MonoBehaviour, INetworkRunner
{
    [SerializeField] private NetworkRunner _runner;
    [SerializeField] private NetworkCore _core;

    private Dictionary<PlayerRef, Player> _spawnedCharacters = new Dictionary<PlayerRef, Player>();
    private IFactory _factory;

    public NetworkCore Core => _core;

    public NetworkRunner Runner => _runner;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Init(IFactory factory)
    {
        _factory = factory;
    }

    public async void StartGame(GameMode mode, string room)
    {
        _runner.ProvideInput = true;
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode     = mode,
            SessionName  = room,
            Scene        = Idents.Scenes.GameIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public void Left(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out Player networkObject))
        {
            runner.Despawn(networkObject.Object);
            _spawnedCharacters.Remove(player);
        }
    }

    public Player GetAlive()
    {
        foreach (var player in _spawnedCharacters.ToList())
        {
            if (player.Value.IsAlive())
            {
                return player.Value;
            }
        }
        return null;
    }

    public void Kill(Player obj)
    {
        foreach (var player in _spawnedCharacters.ToList())
        {
            if (player.Value == obj)
            {
                _spawnedCharacters.Remove(player.Key);
            }
        }
    }

    public void JoinPlayer(NetworkRunner runner, PlayerRef player)
    {
        InstantiatePlayer(runner, player);
    }

    private void InstantiatePlayer(NetworkRunner runner, PlayerRef player)
    {
        var i = Mathf.Pow(-1, runner.ActivePlayers.Count());
        Vector3 spawnPosition =
            new Vector3( i* 20, -i*10, 0);


        _factory.CreateNetBehavior<Player>(AssetPathes.Network.PlayerPref, spawnPosition, player);
    }

    public void AddPlayer(PlayerRef runnerLocalPlayer, Player player)
    {
        _spawnedCharacters.Add(runnerLocalPlayer, player);
    }
}