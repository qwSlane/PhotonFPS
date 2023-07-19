using System.Threading.Tasks;
using DefaultNamespace;
using DefaultNamespace.Services;
using DefaultNamespace.Services.Factory;
using UnityEngine.SceneManagement;

public class InitLobbyState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly IFactory _factory;

    public InitLobbyState(GameStateMachine stateMachine, IFactory factory)
    {
        _stateMachine = stateMachine;
        _factory      = factory;
    }

    public async void Enter()
    {
        var load = SceneManager.LoadSceneAsync(Idents.Scenes.Lobby);
        while (!load.isDone)
        {
            await Task.Yield();
        }
        InitLobby();
    }

    private void InitLobby()
    {
        var lobbyUI = _factory.Create<LobbyUI>(AssetPathes.UI.LobbyUI);
        lobbyUI.Init(_stateMachine);
    }

    public void Exit()
    {
    }
}