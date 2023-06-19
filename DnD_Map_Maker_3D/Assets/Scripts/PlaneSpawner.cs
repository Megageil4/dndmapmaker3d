using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class responsible for spawning and updating the map 
/// </summary>
public class PlaneSpawner : MonoBehaviour
{
    /// <summary>
    /// The x size of the map
    /// </summary>
    public int sizeX; 
    /// <summary>
    /// The y size of the map
    /// </summary>
    public int sizeY;
    /// <summary>
    /// The mesh used by unity in the background
    /// </summary>
    private Mesh _mesh;
    /// <summary>
    /// All vertices of the map
    /// </summary>
    [FormerlySerializedAs("Vertices")] public Vector3[] vertices;
    /// <summary>
    /// All triangles connecting the vertices of the map
    /// </summary>
    private int[] _triangles;
    /// <summary>
    /// The UVs used to map the texture (grid) onto the map
    /// </summary>
    private Vector2[] _uvs;

    /// <summary>
    /// Method used to generate a completely new map with the X and Y size
    /// </summary>

    public void RegenerateMeshFromStart()
    {
        CreateVertices();
        CreateTriangles();
        GenerateUVs();
        ReloadMesh();
    }
    
    /// <summary>
    /// Used to reload an already existing map after the vertices and triangles have been changed
    /// </summary>
    public void ReloadMesh(bool sendMap = true)
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.vertices = vertices;
        _mesh.triangles = _triangles;
        GenerateUVs();
        _mesh.uv = _uvs;
        _mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = _mesh;
        if (sendMap)
        {
            DataContainer.Conn.SendMap(Util.MeshToMapData(_mesh, sizeX, sizeY));   
        }
    }

    /// <summary>
    /// Creates all vertices of the map according to the X and Y size
    /// </summary>
    private void CreateVertices()
    {
        vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];
        int i = 0;
        for (int y = 0; y <= sizeY; y++)
        {
            for (int x = 0; x <= sizeX; x++)
            {
                vertices[i] = new Vector3(x, 0, y);
                i++;
            }
        }
    }
    
    /// <summary>
    /// Creates all triangles connecting the vertices of the map 
    /// </summary>
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
    /// <summary>
    /// Generates the UVs used to map the texture (grid) onto the map
    /// </summary>
    private void GenerateUVs()
    {
        _uvs = new Vector2[vertices.Length];
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

    /// <summary>
    /// Sets spheres at the position of all vertices
    /// </summary>
    private void OnDrawGizmos()
    {
        if (vertices != null)
        {
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                Gizmos.DrawSphere(vertices[i],0.1f);
            }
        }
    }

    /// <summary>
    /// Makes a new map from a provided map data
    /// </summary>
    /// <param name="map">The map data the map should be made of</param>
    /// <param name="sendMap">If the map data should be uploaded to server</param>
    public void SetNewMap(MapData map, bool sendMap = true)
    {
        sizeX = map.sizeX;
        sizeY = map.sizeY;
        vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];
        int i = 0;
        foreach (var v in map.Vertices)
        {
            // Debug.Log(i);
            vertices[i] = new Vector3(v[0], v[1], v[2]);
            i++;
        }
        _triangles = map.triangles;
        ReloadMesh(sendMap);
    }
}
