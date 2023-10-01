using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRainButton : MonoBehaviour
{
    public GameObject HellManager, Resource;
    void OnMouseDown()
    {
        HellManager hellBox = HellManager.GetComponent<HellManager>();
        if(hellBox.upcomingHell == 0 || hellBox.upcomingHell == 3 || hellBox.upcomingHell == 7) hellBox.guardHell = true;
        Resource.GetComponent<Resource>().grain *= 1.1f;
    }
}
