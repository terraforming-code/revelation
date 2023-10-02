using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechBuildButton : MonoBehaviour
{
    public GameObject techTabPivot, techBuildTab, techIronTab, techToolTab;
    void OnMouseDown()
    {
        techBuildTab.SetActive(true);
        techIronTab.SetActive(false);
        techToolTab.SetActive(false);
        techTabPivot.transform.localPosition = new Vector3(techTabPivot.transform.localPosition.x,this.transform.localPosition.y,0);
    }
}
