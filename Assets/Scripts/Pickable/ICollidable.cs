using DefaultNamespace.Player;

namespace DefaultNamespace.Pickable
{
    public interface ICollidable
    {
        bool Collide(PickHandler handler);
    }
}