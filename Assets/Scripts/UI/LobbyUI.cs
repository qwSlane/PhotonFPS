using DefaultNamespace;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button _createLobby;
    [SerializeField] private Button _enterLobby;

    [SerializeField] private TMP_InputField _createText;
    [FormerlySerializedAs("_inputText")] [SerializeField] private TMP_InputField _enterText;
    [SerializeField] private TMP_InputField _nickField;

    private GameStateMachine _stateMachine;

    public void Init(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _createLobby.onClick.AddListener(CreateLobby);
        _enterLobby.onClick.AddListener(EnterLobby);
        //Debug.Log("UI initialized");
    }

    public void CreateLobby()
    {
        var data = new GameData(GameMode.Host, _createText.text);
        SaveData();
        _stateMachine.Enter<LoadGameState, GameData>(data);
    }

    public void EnterLobby()
    {
        var data = new GameData(GameMode.Client, _enterText.text);
        SaveData();
        _stateMachine.Enter<LoadGameState, GameData>(data);
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("NickName", _nickField.text);
        PlayerPrefs.Save();
    }
}