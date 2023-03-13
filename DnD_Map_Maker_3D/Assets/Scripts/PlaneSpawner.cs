using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaneSpawner : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    private Mesh _mesh;
    public Vector3[] Vertices;
    private int[] _triangles;
    private Vector2[] _uvs;
    void Start()
    {
        RegenerateMeshFromStart();
    }

    public void RegenerateMeshFromStart()
    {
        CreateVertices();
        CreateTriangles();
        GenerateUVs();
        ReloadMesh();
    }
    
    public void ReloadMesh()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.vertices = Vertices;
        _mesh.triangles = _triangles;
        _mesh.uv = _uvs;
        _mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = _mesh;
    }

    private void CreateVertices()
    {
        Vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];
        int i = 0;
        for (int y = 0; y <= sizeY; y++)
        {
            for (int x = 0; x <= sizeX; x++)
            {
                Vertices[i] = new Vector3(x, 0, y);
                i++;
            }
        }
    }
    
    
    private void CreateTriangles()
    {
        _triangles = new int[sizeX * sizeY * 6];
        int vert = 0; 
        int tri = 0;
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                _triangles[tri + 0] = vert + 0;
                _triangles[tri + 1] = vert + sizeX + 1;
                _triangles[tri + 2] = vert + 1;
            
                _triangles[tri + 3] = vert + 1;
                _triangles[tri + 4] = vert + sizeX + 1;
                _triangles[tri + 5] = vert + sizeX + 2;

                tri += 6;
                vert++;
            }

            vert++;
        }
    }

    private void GenerateUVs()
    {
        _uvs = new Vector2[Vertices.Length];
        int i = 0;
        for (int y = 0; y <= sizeY; y++)
        {
            for (int x = 0; x <= sizeX; x++)
            {
                _uvs[i] = new Vector2(x % 2, y % 2);
                i++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Vertices != null)
        {
            for (int i = 0; i < Vertices.GetLength(0); i++)
            {
                Gizmos.DrawSphere(Vertices[i],0.1f);
            }
        }
    }

    public void setNewMap(MapData map)
    {
        Debug.Log("setting map");
        int i = 0;
        foreach (var v in map.Vertices)
        {
            Debug.Log("1 : " + i);
            Vertices[i] = new Vector3(v[0], v[1], v[2]);
            Debug.Log("2 : " + i);
            i++;
            Debug.Log("3 : " + i);
        }
        _triangles = map.Triangles;
        sizeX = Vertices.GetLength(0)-1;
        sizeY = Vertices.GetLength(1)-1;
        Debug.Log(sizeX);
        Debug.Log(sizeY);
    }
}
