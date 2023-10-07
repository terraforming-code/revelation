    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject Resource, SeasonManager, CitizenManager;
    CitizenManager citizenBox;
    Resource resource;
    SeasonManager seasonBox;
    public float[] build = new float[]{-1f,-1f,-1f,-1f,-1f,-1f,-1f};
    GameObject[] buildObj = new GameObject[]{null,null,null,null,null,null,null};

    public int buildMouseFocus = -1; // related with BuildingBriefTab
    
    float Bedishappy = 0f, Homeislove = 0f;
    void Start()
    {
        resource = Resource.GetComponent<Resource>();
        citizenBox = CitizenManager.GetComponent<CitizenManager>();
        seasonBox = SeasonManager.GetComponent<SeasonManager>();
        for(int i = 0; i < buildObj.Length; i++)
        {
            buildObj[i] = this.transform.GetChild(i).GetChild(0).gameObject;
            //buildBar[i] = this.transform.GetChild(i).GetChild(1).gameObject;
        }
    }
    void Update()
    {
        if(build[2] == 1f)
        {
            Homeislove += Time.deltaTime/seasonBox.gamespeed;
            if(Homeislove >= 2f)
            {
                Homeislove = 0f;
                citizenBox.citizenAdd();
            }
        }
        if(build[3] == 1f)
        {
            Bedishappy += Time.deltaTime/seasonBox.gamespeed;
            if(Bedishappy >= 1f)
            {
                Bedishappy = 0f;
                resource.happy = Mathf.Min(resource.happy+0.05f,1.2f);
            }
        }
        for(int i = 0; i < 6; i++)
        {
            if(build[i] >= 0f && build[i] < 1f)
            {
                build[i] += Time.deltaTime/seasonBox.gamespeed;
                if(build[i] > 1f)
                {
                    build[i]=1f;
                }
            }
        }
    }
    public void DestroyBuilding(int num) {
        
    }
}
