using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopButton : MonoBehaviour
{
    public GameObject seasonManagerObj;
    void OnMouseDown()
    {
        SeasonManager seasonManager = seasonManagerObj.GetComponent<SeasonManager>();
        if(seasonManager.seasonstop < 0f)
        {
            seasonManager.seasonstop = 0f;
        }
    }
}
