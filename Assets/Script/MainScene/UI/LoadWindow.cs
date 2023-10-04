using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWindow : Window
{
    public override string WindowName{get{return "Load";}}
    public override void Start()
    {
        base.Start();
        var files = SaveManager.GetFiles();
        Debug.Log("LoadManager: List of Files");
        foreach (var file in files)
        {
            Debug.Log("{0}"+file);
        }
    }
    void Update()
    {
        
    }
}
