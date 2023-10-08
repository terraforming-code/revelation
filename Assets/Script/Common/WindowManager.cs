using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindowManager : Singleton<WindowManager>
{
    private Dictionary<string, GameObject> nameToWindow = new Dictionary<string, GameObject>(); 
    public override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Clear()
    {
        nameToWindow.Clear();
    }
    public GameObject Get(string windowName)
    {
        return(nameToWindow[windowName]);
    }
    public void Open(string windowName)
    {
        if(nameToWindow.ContainsKey(windowName))
            nameToWindow[windowName].GetComponent<Window>().Open();
            // nameToWindow[windowName].SetActive(true);
        else
            throw new Exception($"window {windowName} is not registered.");
    }
    public void Add(string windowName, GameObject window)
    {
        // if(!windowName.ContainsKey(windowName)){
            Debug.Log($"WindowManager: Adding windowName={windowName}");
            nameToWindow[windowName] = window;
        // }
    }
}
