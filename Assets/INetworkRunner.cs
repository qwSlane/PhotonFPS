using Fusion;

public interface INetworkRunner : IService
{
    public void StartGame(GameMode mode, string room);
}