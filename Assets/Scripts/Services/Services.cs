public class Services
{
    private static Services _instance;
    public static Services Container => _instance ??= new Services();
    
    private Services(){}
    
    public Services Add<TService>(TService implementation) where TService : IService
    {
        Implementation<TService>.ServiceInstance = implementation;
        return this;
    }

    public TService Get<TService>() where TService : IService =>
        Implementation<TService>.ServiceInstance;

    private class Implementation<TService> where TService : IService

    {
        public static TService ServiceInstance;
    }
}