using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellEventManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HellEventStart()
    {
        this.transform.position = new Vector3(0,0,0);
    }
    public void HellEventEnd()
    {
        this.transform.position = new Vector3(0,-10,0);
    }
}
