using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingMouseEvent : MonoBehaviour
{
    public GameObject buildingBriefTab, Hut;
    HutClickEvent hutClickEvent;
    SpriteRenderer buildingBriefTabObj;
    TextMeshPro buildingBriefTabName;
    public int pivot;
    BuildingManager buildingBox;
    string[] buildingName = new string[]{"Barricade","Dike","Hut","Leaf Bed","Earthenware","Dolmen","Altar"};
    // Start is called before the first frame update
    void Start()
    {
        hutClickEvent = Hut.GetComponent<HutClickEvent>();
        buildingBox = this.transform.parent.parent.GetComponent<BuildingManager>();
        buildingBriefTabObj = buildingBriefTab.transform.GetChild(0).GetComponent<SpriteRenderer>();
        buildingBriefTabName = buildingBriefTab.transform.GetChild(1).GetComponent<TextMeshPro>();
    }

    void OnMouseEnter()
    {
        if(buildingBox.build[pivot] >= -1f && ( (pivot == 3||pivot == 4)? hutClickEvent.Inside : true )) {
            buildingBox.buildMouseFocus = pivot;
            buildingBriefTab.SetActive(true);
            buildingBriefTabObj.sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
            buildingBriefTabName.text = buildingName[pivot];
        }
    }
    void OnMouseOver()
    {
        if(buildingBox.buildMouseFocus == pivot) { // -4.28 ~ 0.39
            buildingBriefTab.transform.GetChild(2).GetChild(0).localPosition = new Vector3(-4.28f + 4.67f*buildingBox.build[pivot],0f,0f);
            buildingBriefTab.transform.GetChild(2).GetChild(0).localScale = new Vector3(buildingBox.build[pivot],1f,1f);
        }
    }
    void OnMouseExit()
    {
        if(buildingBox.buildMouseFocus == pivot) {
            
            buildingBriefTab.SetActive(false);
            buildingBox.buildMouseFocus = -1;
        }
    }
}
