using UnityEngine;

public class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }

            return _instance;
        }
            
    }

    public void Create()
    {
        if (_instance != null)
        {
            return;
        }

        var obj = new GameObject(typeof(T).Name);
        DontDestroyOnLoad(obj);
        _instance = obj.AddComponent<T>();
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}