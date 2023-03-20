using System;
using DefaultNamespace;
using DnD_3D.ServerConnection.Default;
using UnityEngine;

public interface IDnDConnection
{
    void SendMap(MapData map);
    event EventHandler<MapEventArgs> GetMap;
    void AddGameObject(GameObject gameObject);
    event EventHandler<GameObjectEventArgs> GetGameObjects;
    bool MapExists();


    bool Connected();
}