using UnityEngine;

[CreateAssetMenu(fileName = "ObjectListSO", menuName = "Scriptable Objects/ObjectListSO")]
public class ObjectListSO : ScriptableObject
{
    public GameObject[] objectToSpawn;
}
