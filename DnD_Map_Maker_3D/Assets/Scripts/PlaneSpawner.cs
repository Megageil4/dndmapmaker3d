using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaneSpawner : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    private Mesh _mesh;
    [FormerlySerializedAs("Vertices")] public Vector3[] vertices;
    private int[] _triangles;
    private Vector2[] _uvs;
    void Start()
    {
        //RegenerateMeshFromStart();
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
        _mesh.vertices = vertices;
        _mesh.triangles = _triangles;
        GenerateUVs();
        _mesh.uv = _uvs;
        _mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = _mesh;
        
        DataContainer.Conn.SendMap(Util.MeshToMapData(_mesh, sizeX, sizeY));
    }

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
    public void SetNewMap(MapData map)
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
        _triangles = map.Triangles;
        ReloadMesh();
    }
}
