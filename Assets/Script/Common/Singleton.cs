using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    static T _instance;
    public static T Instance => _instance;
    // {
    //     get
    //     {
    //         if(_instance == null) _instance = (T) FindObjectOfType(typeof(T));
    //         if(_instance == null) Debug.LogError("An instance of " + typeof(T) +  " is needed in the scene, but there is none.");
    //         return _instance;
    //     }
    // }

    public virtual void Awake()
    {
        if     ( _instance == null) _instance = this as T;
        else if(_instance != this ) Destroy(this);
    }
    // public virtual void Start()
    // {
    // }
    // public virtual void Update()
    // {        
    // }
}