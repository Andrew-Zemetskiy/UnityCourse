using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    var singletonObj = new GameObject();
                    _instance = singletonObj.AddComponent<T>();
                    singletonObj.name = typeof(T).ToString() + "(Singleton)";

                    DontDestroyOnLoad(singletonObj);
                }
            }

            return _instance;
        }
    }
}