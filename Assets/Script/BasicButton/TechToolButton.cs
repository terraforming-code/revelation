using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechToolButton : MonoBehaviour
{
    public GameObject techTabPivot, techBuildTab, techIronTab, techToolTab;
    void OnMouseDown()
    {
        techBuildTab.SetActive(false);
        techIronTab.SetActive(false);
        techToolTab.SetActive(true);
        techTabPivot.transform.localPosition = new Vector3(techTabPivot.transform.localPosition.x,this.transform.localPosition.y,0);
    }
}
