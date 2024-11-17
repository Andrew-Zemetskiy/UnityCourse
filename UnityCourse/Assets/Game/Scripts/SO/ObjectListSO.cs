using UnityEngine;

[CreateAssetMenu(fileName = "ObjectListSO", menuName = "ScriptableObjects/ObjectListSO")]
public class ObjectListSO : ScriptableObject
{
    public GameObject[] objectToSpawn;
}
