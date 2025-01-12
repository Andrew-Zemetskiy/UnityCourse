using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    public Vector3 CubeSize = new Vector3(2f,1f,2f);
    public Vector3 _targetPoint;
    
    private Mesh _mesh;
    private Vector3 _offset,_firstPoint;

    private void Start()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        _offset = new Vector3(-CubeSize.x / 2, CubeSize.y / 2, -CubeSize.z / 2);
        _firstPoint = _targetPoint + _offset;

        _mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.vertices = GenerateVertices();
        _mesh.triangles = GenerateTriangles();
        _mesh.RecalculateNormals();
    }

    public void SetSize(Vector3 size)
    {
        CubeSize = size;
        GenerateMesh();
    }
    
    private Vector3[] GenerateVertices()
    {
        return new Vector3[]
        {
            _firstPoint,
            _firstPoint + Vector3.forward * CubeSize.z,
            _firstPoint + Vector3.right * CubeSize.x,
            _firstPoint + Vector3.right * CubeSize.x + Vector3.forward * CubeSize.z,

            _firstPoint + Vector3.down * CubeSize.y,
            _firstPoint + Vector3.down * CubeSize.y + Vector3.forward * CubeSize.z,
            _firstPoint + Vector3.down * CubeSize.y + Vector3.right * CubeSize.x,
            _firstPoint + Vector3.down * CubeSize.y + Vector3.right * CubeSize.x + Vector3.forward * CubeSize.z,

            #region Vertices
            // new Vector3(0.0f, 0.0f, 0.0f),
            // new Vector3(0.0f, 0.0f, 1.0f),
            // new Vector3(1.0f, 0.0f, 0.0f),
            // new Vector3(1.0f, 0.0f, 1.0f),
            //
            // new Vector3(0.0f, -1f, 0.0f),
            // new Vector3(0.0f, -1f, 1.0f),
            // new Vector3(1.0f, -1f, 0.0f),
            // new Vector3(1.0f, -1f, 1.0f)
            #endregion
        };
    }

    private int[] GenerateTriangles()
    {
        int[] up = new int[] { 0, 1, 2, 1, 3, 2 };
        int[] front = new int[] { 4, 0, 6, 0, 2, 6 };
        int[] left = new int[] { 6, 2, 7, 2, 3, 7 };
        int[] right = new int[] { 5, 1, 4, 1, 0, 4 };
        int[] back = new int[] { 7, 3, 5, 1, 5, 3 };
        int[] bottom = new int[] { 5, 4, 7, 4, 6, 7 };

        int[] cubeTriangles = up.Concat(front).Concat(left).Concat(right).Concat(back).Concat(bottom).ToArray();
        return cubeTriangles;
    }
}