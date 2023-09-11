using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    public float gamespeed = 16f;
    public float season = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        season += Time.deltaTime/gamespeed*4;
        this.transform.position += new Vector3(Time.deltaTime/gamespeed*7,0,0);
        if(season>4) {
            season = 0f;
            this.transform.position = new Vector3(-3.5f,this.transform.position.y,0);
        }
    }
}
