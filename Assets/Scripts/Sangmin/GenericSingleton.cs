using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : class
{
    private static T _instance;
    public static T Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance == null)
        {
            OnAwake();
            DontDestroyOnLoad(gameObject);
            _instance = GetComponent<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnAwake() { }
    

   
}
