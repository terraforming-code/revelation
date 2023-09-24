using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    public GameObject BGManagerObj;
    BGManager bgManager;

    public float gamespeed = 16f;
    public float season = 0f;
    public float seasonstop = -1f;
    public bool night = false;
    
    // Start is called before the first frame update
    void Start()
    {
        bgManager = BGManagerObj.GetComponent<BGManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(seasonstop >= 0f) {
            seasonstop += Time.deltaTime/gamespeed*4;
            if(seasonstop >= 1f/3) seasonstop = -1f;
        }
        else {
            season += Time.deltaTime/gamespeed*4;
            this.transform.position += new Vector3(Time.deltaTime/gamespeed*7,0,0);
            this.transform.Rotate(0,0,-Time.deltaTime/gamespeed*360);
            if(season-Mathf.Floor(season) < 2f/3f && night)
            {
                night = false;
                bgManager.ChangeSkyNight(night);
            }
            else if(season-Mathf.Floor(season) > 2f/3f && !night) {night = true; bgManager.ChangeSkyNight(night);}
            if(season>4) {
                season = 0f;
                this.transform.position = new Vector3(-3.5f,this.transform.position.y,0);
            }
        }
    }
}
