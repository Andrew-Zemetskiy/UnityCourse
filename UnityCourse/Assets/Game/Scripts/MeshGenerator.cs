using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;

    [SerializeField] private Vector3 CubeSize = new Vector3(2f,8f,4f);
    [SerializeField] private Vector3 _targetPoint;
    
    private Vector3 _offset,_firstPoint;

    private void Start()
    {
        _offset = new Vector3(-CubeSize.x / 2, CubeSize.y / 2, -CubeSize.z / 2);
        _firstPoint = _targetPoint + _offset;

        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateNormals();
    }

    Vector3[] GenerateVertices()
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

            // new Vector3(0.0f, 0.0f, 0.0f),
            // new Vector3(0.0f, 0.0f, 1.0f),
            // new Vector3(1.0f, 0.0f, 0.0f),
            // new Vector3(1.0f, 0.0f, 1.0f),
            //
            // new Vector3(0.0f, -1f, 0.0f),
            // new Vector3(0.0f, -1f, 1.0f),
            // new Vector3(1.0f, -1f, 0.0f),
            // new Vector3(1.0f, -1f, 1.0f)
        };
    }

    int[] GenerateTriangles()
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