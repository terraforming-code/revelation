using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellEventManager : MonoBehaviour
{
    public GameObject hellManager;
    HellManager hellBox;
    SpriteRenderer hellEventIcon, hellEventBG;
    // Start is called before the first frame update
    void Start()
    {
        hellEventIcon = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        hellEventBG = this.transform.GetComponent<SpriteRenderer>();
        hellBox = hellManager.GetComponent<HellManager>();
    }

    public void HellEventStart(int num)
    {
        hellEventIcon.sprite = hellBox.hellSpriteBox[num];
        hellEventBG.color = HellEventBGColor(num);
        this.transform.position = new Vector3(0,0,0);
    }
    public void HellEventEnd()
    {
        this.transform.position = new Vector3(0,-10,0);
    }
    Color HellEventBGColor(int num)
    {
        switch(num)
        {
            case 0:
                return new Color(0.5f,0.25f,0,0.5f);
            case 1:
                return new Color(0.25f,0.25f,1,0.5f);
            case 2:
                return new Color(0.2f,0.2f,0.2f,0.5f);
            case 3:
                return new Color(1,1,0.5f,0.5f);
            case 4:
                return new Color(0.7f,0.7f,1,0.5f);
            case 5:
                return new Color(0.8f,0.8f,0.8f,0.5f);
            case 6:
                return new Color(1,1,1,0.7f);
            case 7:
                return new Color(0.25f,0,0,0.7f);
            case 8:
                return new Color(0.5f,0,0,0.3f);
            case 9:
                return new Color(0.7f,0.7f,1,0.5f);
            case 10:
                return new Color(0.8f,0.8f,0.8f,0);
            case 11:
                return new Color(0.5f,0.5f,0.5f,0.5f);
            case 12:
                return new Color(0.5f,0,0,0.5f);
            case 13:
                return new Color(0.1f,0.3f,0.1f,0.5f);
        }
        return new Color(0,0,0,0);
    }
}
