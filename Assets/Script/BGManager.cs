using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    SpriteRenderer skyRender;
    public Sprite skyDaySprite, skyNightSprite;
    // Start is called before the first frame update
    void Start()
    {
        skyRender = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    public void ChangeSkyNight(bool night) // night = true, day = false
    {
        if(!night) skyRender.sprite = skyDaySprite;
        else skyRender.sprite = skyNightSprite;
    }

}
