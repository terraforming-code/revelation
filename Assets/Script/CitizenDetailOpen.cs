using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CitizenDetailOpen : MonoBehaviour
{
    public GameObject citizenDetail;
    public GameObject citizenManager, Resource;
    public int this_info_label;
    CitizenManager citizenBox;
    Saram saram;
    TextMeshPro nameTitle, charTitle1, charTitle2, charTitle3;
    // Start is called before the first frame update
    void Start()
    {
        citizenBox = citizenManager.GetComponent<CitizenManager>();
        saram = Resource.GetComponent<Saram>();
        nameTitle = citizenDetail.transform.GetChild(0).GetComponent<TextMeshPro>();
        charTitle1 = citizenDetail.transform.GetChild(3).GetComponent<TextMeshPro>();
        charTitle2 = citizenDetail.transform.GetChild(4).GetComponent<TextMeshPro>();
        charTitle3 = citizenDetail.transform.GetChild(5).GetComponent<TextMeshPro>();
    }
    void OnMouseEnter()
    {
        citizenDetail.SetActive(true);
        int innerTempPivot = citizenBox.citizenPivot[citizenBox.tabNum]+this_info_label;
        nameTitle.text = saram.nickname[citizenBox.tabNum][innerTempPivot];
        citizenBox.GaugeScale(citizenDetail.transform.GetChild(1),-2.6f,0.22f,0.6f,saram.farming[citizenBox.tabNum][innerTempPivot]/2.5f /10f);
        citizenBox.GaugeScale(citizenDetail.transform.GetChild(2),-2.6f,0.22f,0.6f,saram.fighting[citizenBox.tabNum][innerTempPivot]/2.5f);
        charTitle1.text = saram.charTag1[saram.char1[citizenBox.tabNum][innerTempPivot]];
        charTitle2.text = saram.charTag2[saram.char2[citizenBox.tabNum][innerTempPivot]];
        charTitle3.text = saram.charTag3[saram.char3[citizenBox.tabNum][innerTempPivot]];
    }
    void OnMouseExit()
    {
        citizenDetail.SetActive(false);
    }
}
