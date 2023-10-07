using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrollButton : MonoBehaviour
{
    public int dir;
    public GameObject BGObj;
    Transform BGSky, BGShadow1, BGShadow2, BGground;
    SpriteRenderer thisSprite;
    bool clicking = false;
    // Start is called before the first frame update
    void Start()
    {
        thisSprite = this.GetComponent<SpriteRenderer>();
        BGSky = BGObj.transform.GetChild(0);
        BGShadow1 = BGObj.transform.GetChild(1).GetChild(0);
        BGShadow2 = BGObj.transform.GetChild(1).GetChild(1);
        BGground = BGObj.transform.GetChild(2);
    }

    
    void Update()
    {
        if((dir==-1 && BGground.localPosition.x <= -7.84f) || (dir==1 && BGground.localPosition.x >= 7.84f)) {
            thisSprite.color = new Color(1,1,1,0.003f);
            clicking = false;
        }
        else if(clicking && ((dir==-1 && BGground.localPosition.x >= -7.84f) || (dir==1 && BGground.localPosition.x <= 7.84f))) {
            BGSky.localPosition += new Vector3( dir * Time.deltaTime ,0,0);
            BGShadow1.localPosition += new Vector3( dir * Time.deltaTime /4f * 7.84f,0,0);
            BGShadow2.localPosition += new Vector3( dir * Time.deltaTime /3f * 7.84f,0,0);
            BGground.localPosition += new Vector3( dir * Time.deltaTime * 7.84f,0,0);
        }
    }
    void OnMouseEnter()
    {
        if((dir==-1 && BGground.localPosition.x >= -7.84f) || (dir==1 && BGground.localPosition.x <= 7.84f)) {
            
            thisSprite.color = new Color(1,1,1,0.25f);
        }
    }
    void OnMouseDown()
    {
        if((dir==-1 && BGground.localPosition.x >= -7.84f) || (dir==1 && BGground.localPosition.x <= 7.84f)) {
            //Debug.Log($"{dir} direction : BGscrollButton OnMouseDown");
            thisSprite.color = new Color(1,1,1,0.5f);
            clicking = true;
        }
    }
    void OnMouseUp()
    {
        thisSprite.color = new Color(1,1,1,0.003f);
        clicking = false;
    }
    void OnMouseExit()
    {
        thisSprite.color = new Color(1,1,1,0.003f);
        clicking = false;
    }
}
