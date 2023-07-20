using DefaultNamespace.Services.Factory;
using DefaultNamespace.UI;
using Fusion;

public interface INetworkRunner : IService
{
    public void StartGame(GameMode mode, string room);
}