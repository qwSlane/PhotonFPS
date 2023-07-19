using DefaultNamespace;
using Fusion;
using UnityEngine;

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