using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMaker : MonoBehaviour
{
    public GameObject raindrop, snowdrop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void makeRain(int num) {
        for(int i = 0; i < num; i ++) {
            GameObject tempRain = Instantiate(raindrop,this.transform);
        }
    }
    public void makeSnow(int num) {
        for(int i = 0; i < num; i ++) {
            GameObject tempRain = Instantiate(snowdrop,this.transform);
            tempRain.GetComponent<Raindrop>().dropSpeed = 1f/6f;
        }
    }
}
