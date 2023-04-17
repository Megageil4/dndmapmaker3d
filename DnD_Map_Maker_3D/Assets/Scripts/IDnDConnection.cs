using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnStuff;
using DefaultNamespace;
using DnD_3D.ServerConnection.Default;
using UnityEngine;

public interface IDnDConnection
{
    void SendMap(MapData map);
    void AddGameObject(GameObject gameObject);
    public List<JK_GameObject> GetGameObjects();
    Task<bool> MapExists();
    List<GameObject> OnConnectGO();

    MapData OnConnectMap();

    bool Connected();

    void Dispose();
}