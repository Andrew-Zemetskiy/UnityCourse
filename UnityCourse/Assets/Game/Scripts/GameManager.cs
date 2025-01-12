using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private Transform _stackParent;
    [SerializeField] private Vector3 _firstBlockSize = new Vector3(2f, 1f, 2f);
    [SerializeField] private float _moveDistance = 4f;
    [SerializeField] private float _speed = 3f;

    [SerializeField] private Transform _viewPoint;
    
    private GameObject _lastBlock;
    private Vector3 _moveDirection;
    private bool _isMoving;

    private void Start()
    {
        SpawnBlock(Vector3.zero, _firstBlockSize);
    }

    private void Update()
    {
        if (_isMoving)
        {
            MoveBlock();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceBlock();
        }
    }

    private void SpawnBlock(Vector3 position, Vector3 size)
    {
        GameObject newBlock = Instantiate(_blockPrefab, position, Quaternion.identity, _stackParent);
        MeshGenerator generator = newBlock.GetComponent<MeshGenerator>();
        generator.SetSize(size); // set size to generator

        _lastBlock = newBlock;
        
        //random dir
        _moveDirection = GetRandomDirection();
        _isMoving = true;
        
        MoveUpViewPoint();
    }

    private void MoveBlock()
    {
        Vector3 pos = _lastBlock.transform.position;
        pos += _moveDirection * (_speed * Time.deltaTime);
        
        if (Mathf.Abs(pos.x) > _moveDistance || Mathf.Abs(pos.z) > _moveDistance)
        {
            _moveDirection = -_moveDirection;
        }

        _lastBlock.transform.position = pos;
    }

    private void PlaceBlock()
    {
        _isMoving = false;
        
        //Trim
        Vector3 overlapSize = CalculateOverlap();
        if (overlapSize == Vector3.zero)
        {
            Debug.LogError("Game Over");
            return;
        }
        
        Vector3 newPosition = _lastBlock.transform.position + Vector3.up * (overlapSize.y);
        SpawnBlock(newPosition, overlapSize);
        
        AdjustPreviousBlock(overlapSize);
    }

    private void AdjustPreviousBlock(Vector3 newSize)
    {
        if (_stackParent.childCount < 2) return;

        GameObject previousBlock = _stackParent.GetChild(_stackParent.childCount - 2).gameObject;
        MeshGenerator generator = previousBlock.GetComponent<MeshGenerator>();
        
        generator.SetSize(newSize);
        
        Vector3 shift = CalculateShift(previousBlock.transform.position, newSize);
        shift.y = 0;
        
        previousBlock.transform.position += shift;
    }
    
    private Vector3 CalculateOverlap()
    {
        Vector3 lastPos = _lastBlock.transform.position;
        Vector3 lastSize = _lastBlock.GetComponent<MeshGenerator>().CubeSize;
        
        if (_stackParent.childCount < 2) return lastSize;
        
        GameObject prevBlock = _stackParent.GetChild(_stackParent.childCount - 2).gameObject;
        Vector3 prevPos = prevBlock.transform.position;
        Vector3 prevSize = prevBlock.GetComponent<MeshGenerator>().CubeSize;
        
        float overlapX = Mathf.Min(lastPos.x + lastSize.x / 2, prevPos.x + prevSize.x / 2) - Mathf.Max(lastPos.x - lastSize.x / 2, prevPos.x - prevSize.x / 2);
        float overlapZ = Mathf.Min(lastPos.z + lastSize.z / 2, prevPos.z + prevSize.z / 2) - Mathf.Max(lastPos.z - lastSize.z / 2, prevPos.z - prevSize.z / 2);
        
        if (overlapX <= 0 || overlapZ <= 0) return Vector3.zero;
        
        return new Vector3(overlapX, lastSize.y, overlapZ);
    }

    private Vector3 CalculateShift(Vector3 previousPos, Vector3 newSize)
    {
        Vector3 previousSize = _lastBlock.GetComponent<MeshGenerator>().CubeSize;
        
        float shiftX = (previousPos.x + previousSize.x / 2) - (previousPos.x + newSize.x / 2);
        float shiftZ = (previousPos.z + previousSize.z / 2) - (previousPos.z + newSize.z / 2);

        return new Vector3(shiftX, 0, shiftZ);
    }

    private Vector3 GetRandomDirection()
    {
        return UnityEngine.Random.value > 0.5f ? Vector3.right : Vector3.forward;
    }

    private void MoveUpViewPoint()
    {
        _viewPoint.transform.position += Vector3.up;
    }
}
