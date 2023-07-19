using Fusion;

namespace DefaultNamespace
{
    public struct GameData
    {
        public GameMode GameMode;
        public string LobbyName;

        public GameData(GameMode mode, string name)
        {
            GameMode  = mode;
            LobbyName = name;
        }
    }
}