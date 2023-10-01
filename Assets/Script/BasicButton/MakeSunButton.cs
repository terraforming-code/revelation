using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSunButton : MonoBehaviour
{
    public GameObject HellManager, Resource;
    void OnMouseDown()
    {
        HellManager hellBox = HellManager.GetComponent<HellManager>();
        if(hellBox.upcomingHell == 4 || hellBox.upcomingHell == 6) hellBox.guardHell = true;
        Resource.GetComponent<Resource>().grain *= 1.05f;
    }
}
