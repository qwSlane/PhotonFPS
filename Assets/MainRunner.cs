using DefaultNamespace;
using DefaultNamespace.Services;
using DefaultNamespace.Services.Factory;
using DefaultNamespace.UI;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainRunner : MonoBehaviour, INetworkRunner
{
    [SerializeField] private NetworkRunner _runner;
    [SerializeField] private NetworkCore _core;

    public NetworkCore Core => _core;

    public NetworkRunner Runner => _runner;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public async void StartGame(GameMode mode, string room)
    {
        _runner.ProvideInput = true;
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode     = mode,
            SessionName  = "room",
            Scene        = Idents.Scenes.GameIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
}