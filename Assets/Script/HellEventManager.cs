using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellEventManager : MonoBehaviour
{
    
    int hellEventProgress = 0; // 0 : not in progress, 1 : starting motion, 2 : maintenence motion, 3 : stopping motion
    int hellEventKind = -1;
    public GameObject buildingManager, hellManager, RainMaker;
    public GameObject BG, river, sun, forest, riverFrozen, satanSun;
    public GameObject[] idol = new GameObject[]{null,null};
    public GameObject[] hellGroundBox = new GameObject[]{null,null,null,null,null,null,null,null,null,null,null,null,null,null};
    public SpriteRenderer hellGroundSprite;
    RainMaker rainMaker;
    BuildingManager buildingBox;
    HellManager hellBox;
    SpriteRenderer hellEventIcon, hellEventBG;

    float eventspeed = 1f;

    float quakeMotion = 0f, rainCounter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        hellEventIcon = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        hellEventBG = this.transform.GetComponent<SpriteRenderer>();
        buildingBox = buildingManager.GetComponent<BuildingManager>();
        hellBox = hellManager.GetComponent<HellManager>();
        rainMaker = RainMaker.GetComponent<RainMaker>();
    }
    void Update()
    {
        rainCounter += Time.deltaTime;
        if(hellEventProgress == 1) // going to motion
        {
            hellEventBG.color += new Color(0f,0f,0f,HellEventBGColor(hellEventKind).a * Time.deltaTime * eventspeed);
            switch(hellEventKind)
            {
                case 0 :
                    sun.transform.localPosition += new Vector3(0,eventspeed * Time.deltaTime*-6,0);
                    break;
                case 1 :
                    if(rainCounter > 0.1f) {rainMaker.makeRain(20); rainCounter=0f;}
                    river.transform.localPosition += new Vector3(0,eventspeed * Time.deltaTime*3,0);
                    break;
                case 2 :
                    if(rainCounter > 0.1f) {rainMaker.makeRain(20); rainCounter=0f;}
                    break;
                case 3 :
                    forest.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,eventspeed * Time.deltaTime);
                    river.transform.localPosition -= new Vector3(0,eventspeed * Time.deltaTime*3,0);
                    groundColorChange(true);
                    break;
                case 4 :
                    riverFrozen.GetComponent<SpriteRenderer>().color += new Color(0,0,0,eventspeed * Time.deltaTime);
                    groundColorChange(true);
                    break;
                case 5 :
                    if(rainCounter > 0.1f) {rainMaker.makeSnow(5); rainCounter=0f;}
                    riverFrozen.GetComponent<SpriteRenderer>().color += new Color(0,0,0,eventspeed * Time.deltaTime);
                    groundColorChange(true);
                    break;
                case 6 :
                    if(rainCounter > 0.1f) {rainMaker.makeSnow(5); rainCounter=0f;}
                    riverFrozen.GetComponent<SpriteRenderer>().color += new Color(0,0,0,eventspeed * Time.deltaTime);
                    groundColorChange(true);
                    break;
                case 9 :
                    if(buildingBox.build[6] < 1f) idol[0].GetComponent<SpriteRenderer>().color += new Color(0,0,0,eventspeed * Time.deltaTime);
                    else idol[1].GetComponent<SpriteRenderer>().color += new Color(0,0,0,eventspeed * Time.deltaTime);
                    satanSun.transform.localPosition += new Vector3(0,eventspeed * Time.deltaTime*-6,0);
                    if(hellEventBG.color.a >= HellEventBGColor(hellEventKind).a) {
                        idol[0].GetComponent<SpriteRenderer>().color = new Color(0,0,0,(buildingBox.build[6] < 1f)? 1f : 0f);
                        idol[1].GetComponent<SpriteRenderer>().color = new Color(0,0,0,(buildingBox.build[6] < 1f)? 0f : 1f);
                    }
                    break;
                case 13 :
                    quakeMotion = 0f;
                    hellEventProgress = 2;
                    break;
            }
            if(hellEventBG.color.a >= HellEventBGColor(hellEventKind).a) hellEventProgress = 2;
        }
        if(hellEventProgress == 2) // maintenence motion
        {
            switch(hellEventKind)
            {
                case 0 :
                    sun.transform.Rotate(new Vector3(0f,0f,90f) * Time.deltaTime);
                    break;
                case 1 :
                    if(rainCounter > 0.1f) {rainMaker.makeRain(20); rainCounter=0f;}
                    break;
                case 2 :
                    if(rainCounter > 0.1f) {rainMaker.makeRain(20); rainCounter=0f;}
                    break;
                case 5 :
                    if(rainCounter > 0.1f) {rainMaker.makeSnow(5); rainCounter=0f;}
                    break;
                case 6 :
                    if(rainCounter > 0.1f) {rainMaker.makeSnow(5); rainCounter=0f;}
                    break;
                case 13 :
                    quakeMotion += Time.deltaTime;
                    if(quakeMotion>=1f) {quakeMotion = 0f;}
                    else if(quakeMotion>=4f/5f) {BG.transform.position-=new Vector3(0.5f*Time.deltaTime,0,0);}
                    else if(quakeMotion>=3f/5f) {BG.transform.position+=new Vector3(0.5f*Time.deltaTime,0,0);}
                    else if(quakeMotion>=2f/5f) {BG.transform.position-=new Vector3(0.5f*Time.deltaTime,0,0);}
                    else if(quakeMotion>=1f/5f) {BG.transform.position+=new Vector3(0.5f*Time.deltaTime,0,0);}
                    break;
            }
        }
        if(hellEventProgress == 3) // going to ending motion
        {
            hellEventBG.color -= new Color(0f,0f,0f,HellEventBGColor(hellEventKind).a * Time.deltaTime * eventspeed * 2f);
            switch(hellEventKind)
            {
                case 0 :
                    sun.transform.localPosition += new Vector3(0,eventspeed * Time.deltaTime*6 *2f,0);
                    
                    break;
                case 1 :
                    river.transform.localPosition += new Vector3(0,eventspeed * Time.deltaTime*-3 *2f,0);
                    
                    break;
                case 2 :
                    break;
                case 3 :
                    forest.GetComponent<SpriteRenderer>().color += new Color(0,0,0,eventspeed * Time.deltaTime * 2f);
                    river.transform.localPosition += new Vector3(0,eventspeed * Time.deltaTime*3 *2f,0);
                    groundColorChange(false);
                    break;
                case 4 :
                    riverFrozen.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,eventspeed * Time.deltaTime * 2f);
                    groundColorChange(false);
                    break;
                case 5 :
                    riverFrozen.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,eventspeed * Time.deltaTime * 2f);
                    groundColorChange(false);
                    break;
                case 6 :
                    riverFrozen.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,eventspeed * Time.deltaTime * 2f);
                    groundColorChange(false);
                    break;
                case 9 :
                    idol[0].GetComponent<SpriteRenderer>().color -= new Color(0,0,0,eventspeed * Time.deltaTime);
                    idol[1].GetComponent<SpriteRenderer>().color -= new Color(0,0,0,eventspeed * Time.deltaTime);
                    satanSun.transform.localPosition += new Vector3(0,eventspeed * Time.deltaTime*6,0);
                    break;
                case 13 :
                    BG.transform.position = new Vector3(0,0,0);
                    break;
            }
            if(hellEventBG.color.a <= 0f) { // motion ended
                hellEventProgress = 0;
                this.transform.position = new Vector3(0,-10,0);
                switch(hellEventKind)
                {
                    case 0 :
                        sun.transform.localPosition = new Vector3(0,8,0);
                        
                        break;
                    case 1 :
                        river.transform.localPosition = new Vector3(0,-6,0);
                        
                        break;
                    case 2 :
                        break;
                    case 3 :
                        forest.GetComponent<SpriteRenderer>().color = 
                        new Color(forest.GetComponent<SpriteRenderer>().color.r,forest.GetComponent<SpriteRenderer>().color.g,
                        forest.GetComponent<SpriteRenderer>().color.b,1);

                        river.transform.localPosition = new Vector3(0,-6f,0);
                        groundColorChange(false);
                        break;
                    case 4 :
                        riverFrozen.GetComponent<SpriteRenderer>().color *= new Color(1f,1f,1f,0f);
                        groundColorChange(false);
                        break;
                    case 5 :
                        riverFrozen.GetComponent<SpriteRenderer>().color *= new Color(1f,1f,1f,0f);
                        groundColorChange(false);
                        break;
                    case 6 :
                        riverFrozen.GetComponent<SpriteRenderer>().color *= new Color(1f,1f,1f,0f);
                        groundColorChange(false);
                        break;
                    case 9 :
                        idol[0].GetComponent<SpriteRenderer>().color *= new Color(1f,1f,1f,0f);
                        idol[1].GetComponent<SpriteRenderer>().color *= new Color(1f,1f,1f,0f);
                        satanSun.transform.localPosition = new Vector3(0,8,0);
                        break;
                }
            }
        }
    }
    void groundColorChange(bool starting)
    {
        if(starting) {
            hellGroundSprite.color += new Color(0,0,0,eventspeed * Time.deltaTime );
        }
        else {
            hellGroundSprite.color -= new Color(0,0,0,eventspeed * Time.deltaTime* 2f );
        }
    }
    public void HellEventStart(int num)
    {
        hellEventKind = num;
        if(hellGroundBox[num] != null) hellGroundSprite = hellGroundBox[num].GetComponent<SpriteRenderer>();
        hellEventProgress = 1;
        //hellEventIcon.sprite = hellBox.hellSpriteBox[num];
        hellEventBG.color = new Color(1f,1f,1f,0f) * HellEventBGColor(num);
        this.transform.position = new Vector3(0,0,0);
    }
    public void HellEventEnd()
    {
        hellEventProgress = 3;
        
    }
    Color HellEventBGColor(int num)
    {
        switch(num)
        {
            case 0:
                return new Color(0.5f,0.25f,0,0.2f);
            case 1:
                return new Color(0.25f,0.25f,1,0.2f);
            case 2:
                return new Color(0.2f,0.2f,0.2f,0.2f);
            case 3:
                return new Color(1,1,0.5f,0.2f);
            case 4:
                return new Color(0.7f,0.7f,1,0.2f);
            case 5:
                return new Color(0.8f,0.8f,0.8f,0.2f);
            case 6:
                return new Color(1,1,1,0.4f);
            case 7:
                return new Color(0.25f,0,0,0.4f);
            case 8:
                return new Color(0.5f,0,0,0.1f);
            case 9:
                return new Color(0.7f,0.7f,1,0.2f);
            case 10:
                return new Color(0.8f,0.8f,0.8f,0);
            case 11:
                return new Color(0.5f,0.5f,0.5f,0.2f);
            case 12:
                return new Color(0.5f,0,0,0.2f);
            case 13:
                return new Color(0.1f,0.3f,0.1f,0.2f);
        }
        return new Color(0,0,0,0);
    }
}
