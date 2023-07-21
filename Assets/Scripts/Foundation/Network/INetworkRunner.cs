using Fusion;

public interface INetworkRunner : IService
{
    public void StartGame(GameMode mode, string room);
    public Player GetAlive();
    public void AddPlayer(PlayerRef runnerLocalPlayer, Player player);
}