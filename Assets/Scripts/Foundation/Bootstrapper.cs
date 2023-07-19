using DefaultNamespace;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private void Awake()
    {
        var game = new Game(gameData);
        game.Init();
        game.Start();
        
        DontDestroyOnLoad(this);
    }
}