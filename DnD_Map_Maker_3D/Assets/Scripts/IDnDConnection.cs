using System;
using System.Collections.Generic;
using ConnStuff;
using DefaultNamespace;
using UnityEngine;

public interface IDnDConnection
{
    void SendMap(MapData map);
    void AddGameObject(GameObject gameObject);
    public void ChangeGameObject(Guid guid);
    public List<JKGameObject> GetGameObjects();
    bool MapExists();
    MapData OnConnectMap();
    void Connect();
    void Dispose();
}