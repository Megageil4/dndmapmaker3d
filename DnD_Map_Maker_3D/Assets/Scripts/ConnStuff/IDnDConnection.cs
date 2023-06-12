using System;
using System.Collections.Generic;
using ConnStuff;
using DefaultNamespace;
using UnityEngine;

/// <summary>
/// Interface that defines the methods needed for a connection to the server
/// </summary>
public interface IDnDConnection
{
    void SendMap(MapData map);
    void AddGameObject(GameObject gameObject);
    public void ChangeGameObject(Guid guid);
    public List<JKGameObject> GetGameObjects();
    bool MapExists();
    MapData FetchMap();
    string Connect();
}