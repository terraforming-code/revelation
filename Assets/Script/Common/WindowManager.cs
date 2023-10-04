using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            nameToWindow[windowName].SetActive(true);
    }
    public void Add(string windowName, GameObject window)
    {
        Debug.Log($"WindowManager: windowName={windowName}");
        nameToWindow.Add(windowName, window);
    }
}
