using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ObjectListSO objectListSO;
    [SerializeField] private MaterialsSO materialsSO;

    [SerializeField] private GameObject[] objectArray;
    
    private int _currentIndex = -1;

    private GameObject _currentObject;

    private void Start()
    {
        NavigationUI.OnNavigate += NavigationSpawn;
        NavigationSpawn(true); //spawn first ship at index 0

        NavigationUI.OnColorChanged += ColorChange;
    }

    private void NavigationSpawn(bool isForward) //true - forward, false - back
    {
        GameObject[] objectsList = objectArray ; //objectListSO.objectToSpawn
        if (_currentObject != null)
        {
            Destroy(_currentObject);
        }

        _currentIndex = (_currentIndex + (isForward ? 1 : -1)) % objectsList.Length;
        if (_currentIndex < 0) _currentIndex = objectsList.Length - 1;

        _currentObject = Instantiate(objectsList[_currentIndex], spawnPoint.position, Quaternion.Euler(0f, 180f, 0f),
            transform);
    }

    private void ColorChange(ColorLib color)
    {
        if (_currentObject == null)
            return;

        if (_currentObject.TryGetComponent(out Renderer renderer))
        {
            renderer.sharedMaterial.color = materialsSO.GetColor(color);
        }
    }

    private void OnDestroy()
    {
        NavigationUI.OnNavigate -= NavigationSpawn;
    }
}