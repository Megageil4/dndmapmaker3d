using DnD_3D.ServerConnection.Entity;
namespace DnD_3D.ServerConnection.Default;
public interface IDnDConnection
{
    void SendMap(Map map);
    event EventHandler<MapEventArgs> GetMap;
    void AddGameObject(GameObject gameObject);
    event EventHandler<GameObjectEventArgs> GetGameObjects;
    bool MapExists();

    List<GameObject> OnConnectGO();

    Map OnConnectMap();

    bool Connected();

    void Dispose();
}