using DefaultNamespace;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button _createLobby;
    [SerializeField] private Button _enterLobby;

    [SerializeField] private TMP_InputField _text;

    private GameStateMachine _stateMachine;

    public void Init(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _createLobby.onClick.AddListener(CreateLobby);
        _enterLobby.onClick.AddListener(EnterLobby);
        Debug.Log("UI initialized");
    }

    public void CreateLobby()
    {
        var data = new GameData(GameMode.Host, _text.text);
        _stateMachine.Enter<LoadGameState, GameData>(data);
    }

    public void EnterLobby()
    {
        var data = new GameData(GameMode.Client, _text.text);
        _stateMachine.Enter<LoadGameState, GameData>(data);
    }
}