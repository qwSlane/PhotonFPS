using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    public MainRunner NetworkRunnerPrefab;
    public LobbyUI uipref;
    private Game _game;
    
    private void Awake()
    {
        _game = new Game(Instantiate(NetworkRunnerPrefab));
        _game.Start();
        
        DontDestroyOnLoad(this);
    }
}