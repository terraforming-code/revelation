using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    public GameObject effectManager, NightFilter, Resource, auroraObj;
    SpriteRenderer NightFilterSprite, AuroraSprite;
    Saram saram;
    EffectManager effectBox;
    public float gamespeed = 16f;
    public float season = 0f;
    public float seasonstop = -1f;
    public bool nightTrigger = true;
    float nightX;
    
    // Start is called before the first frame update
    void Start()
    {
        NightFilterSprite = NightFilter.GetComponent<SpriteRenderer>();
        AuroraSprite = auroraObj.GetComponent<SpriteRenderer>();
        effectBox = effectManager.GetComponent<EffectManager>();
        saram = Resource.GetComponent<Saram>();
    }

    // Update is called once per frame
    void Update()
    {
        if(seasonstop >= 0f) {
            seasonstop += Time.deltaTime/gamespeed*4;
            if(seasonstop >= 1f/3) seasonstop = -1f;
        }
        else {
            season += Time.deltaTime/gamespeed*4;
            this.transform.position += new Vector3(Time.deltaTime/gamespeed*7,0,0);
            this.transform.Rotate(0,0,-Time.deltaTime/gamespeed*360);
            if(season-Mathf.Floor(season) < 2f/3f && !nightTrigger)
            {
                nightTrigger = true;
                auroraObj.SetActive(false);
                NightFilterSprite.color = new Color(0f,0.05f,0.27f,0f);
            }
            else if(season-Mathf.Floor(season) > 2f/3f) {
                if(nightTrigger)
                {
                    nightTrigger = false;
                    
                    if(effectBox.enable[12] == 1 && Random.Range(0,10) == 7) {
                        auroraObj.SetActive(true);
                        for(int i = 0; i < 3; i ++) {
                            for(int j = 0; j < saram.num[i]; j ++) {
                                saram.holy[i][j] = Mathf.Min(1, saram.holy[i][j]*1.15f);
                            }
                        }
                    }
                }
                nightX = (season-Mathf.Floor(season)-2f/3f);
                NightFilterSprite.color = new Color(0f,0.05f,0.27f, Mathf.Pow( (1f/3f-nightX)*nightX*36f , 1f/2f)*0.8f );
                AuroraSprite.color = new Color(AuroraSprite.color.r,AuroraSprite.color.g,AuroraSprite.color.b, Mathf.Pow( (1f/3f-nightX)*nightX*36f , 1f/2f) );
            }
            if(season>4) {
                season = 0f;
                this.transform.position = new Vector3(-3.5f,this.transform.position.y,0);
            }
        }
    }
}
