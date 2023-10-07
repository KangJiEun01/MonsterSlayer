using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : class
{
    private static T _instance;
    public static T Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = GetComponent<T>();
            OnAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnAwake() { }
    

   
}
