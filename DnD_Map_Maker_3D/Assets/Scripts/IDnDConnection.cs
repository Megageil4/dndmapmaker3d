using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using DnD_3D.ServerConnection.Default;
using UnityEngine;

public interface IDnDConnection
{
    void SendMap(MapData map);
    event EventHandler<MapEventArgs> GetMap;
    void AddGameObject(GameObject gameObject);
    event EventHandler<GameObjectEventArgs> GetGameObjects;
    Task<bool> MapExists();

    List<GameObject> OnConnectGO();

    MapData OnConnectMap();

    bool Connected();

    void Dispose();
}