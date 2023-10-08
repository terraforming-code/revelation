using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HellManager : MonoBehaviour
{
    public GameObject Resource, SeasonManager, CitizenManager, buildManager, enemyManager, mammothManager,helleventManager,techManager,effectManager;
    public Sprite hellHappyDaySprite;
    HellEventManager hellEventBox;
    EffectManager effectBox;
    TechManager techBox;
    MammothManager mammothBox;
    Saram saram;
    EnemyManager enemyBox;
    BuildingManager buildBox;
    CitizenManager citizenBox;
    Resource resource;
    SeasonManager seasonBox;
    public SpriteRenderer hellSprite;
    public TextMeshPro hellPriceText;
    
    public Sprite[] hellSpriteBox = new Sprite[]{null,null,null,null,null,null,null,null,null,null,null,null,null,null};
    public int upcomingHell = -1;
    public float hellDie = 0f, hellDieNum = 3f; // if value is 1 someone die, killing disaster increases this value
    public float hellDestroy = 0f;
    public bool guardHell = true;
    public bool eyeOn = false; // find inner bad man by spy card
    public bool bigLose = false;
    public bool hellLazy = false;
    public bool experienceFlood = false;
    public int hellPrice;

    bool eventTriggerOn = false;
    float hellEventCounter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        hellEventBox = helleventManager.GetComponent<HellEventManager>();
        effectBox = effectManager.GetComponent<EffectManager>();
        techBox = techManager.GetComponent<TechManager>();
        resource = Resource.GetComponent<Resource>();
        mammothBox = mammothManager.GetComponent<MammothManager>();
        citizenBox = CitizenManager.GetComponent<CitizenManager>();
        seasonBox = SeasonManager.GetComponent<SeasonManager>();
        buildBox = buildManager.GetComponent<BuildingManager>();
        enemyBox = enemyManager.GetComponent<EnemyManager>();
        saram = Resource.GetComponent<Saram>();
        hellSprite = this.GetComponent<SpriteRenderer>();
        hellSprite.sprite = null;
        hellPriceText = this.transform.parent.parent.GetChild(1).GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!guardHell)
        {
            
            switch(upcomingHell)
            {
                case 0 :
                    hellDie += Time.deltaTime/seasonBox.gamespeed*4*hellDieNum;
                    break;
                case 1 :
                    hellDie += Time.deltaTime/seasonBox.gamespeed*4*hellDieNum;
                    resource.grain -= Time.deltaTime/seasonBox.gamespeed*4*hellDieNum * 9;
                    experienceFlood = true;
                    break;
                case 2 :
                    hellDie += Time.deltaTime/seasonBox.gamespeed*4*hellDieNum * 1.5f;
                    break;
                case 3 :
                    resource.grain -= Time.deltaTime/seasonBox.gamespeed*4*hellDieNum * 9 * 2f;
                    break;
                case 4 :
                    hellDie += Time.deltaTime/seasonBox.gamespeed*4*hellDieNum;
                    break;
                case 5 :
                    resource.food -= Time.deltaTime/seasonBox.gamespeed*4*hellDieNum * 9 * 2f;
                    break;
                case 6 :
                    hellDestroy += Time.deltaTime/seasonBox.gamespeed*12 * 1.5f;
                    resource.grain -= Time.deltaTime/seasonBox.gamespeed*4*hellDieNum * 9;
                    break;
                case 7 :
                    hellDestroy += Time.deltaTime/seasonBox.gamespeed*12 * Random.Range(1f,3f);
                    if(hellDestroy >= 1f) hellDie = 1f;
                    break;
                case 13 :
                    hellDestroy += Time.deltaTime/seasonBox.gamespeed*12 * Random.Range(1f,3f);
                    break;
            }
            if(hellDie >= 1f) {
                hellDie = 0f;
                int tempRandomCitizen;
                tempRandomCitizen = Random.Range(0,saram.num[0]+saram.num[1]+saram.num[2]);
                if(tempRandomCitizen < saram.num[0]) citizenBox.citizenKill(0,0,4);
                else if(tempRandomCitizen < saram.num[1]+saram.num[0]) citizenBox.citizenKill(1,tempRandomCitizen-saram.num[0],4);
                else citizenBox.citizenKill(2,tempRandomCitizen-saram.num[0]-saram.num[1],4);
            }
            if(hellDestroy >= 1f) {
                hellDestroy = 0f;
                buildBox.DestroyBuilding(-1);
            }

        }
        else {
            if(eventTriggerOn && HellEventEndCondition()) {
                eventTriggerOn = false;
                hellEventBox.HellEventEnd();
            }
        }
        
    }
    public bool HellGuardCondition()
    {
        if(!eventTriggerOn && upcomingHell != -1) {
            eventTriggerOn = true;
            hellEventBox.HellEventStart(upcomingHell);
        }
        switch(upcomingHell)
        {
            case 0 :
                if(buildBox.build[2]==1f) return true;
                else {resource.happy = Mathf.Max(0.8f,resource.happy-0.1f); return false;}
            case 1 :
                if(buildBox.build[1]==1f) return true;
                else return false;
            case 2 :
                if(buildBox.build[1]==1f) {buildBox.DestroyBuilding(-1); return true;}
                else {buildBox.DestroyBuilding(-1); return false;}
            case 3 :
                return false; // need artificial rain trigger
            case 4 :
                if(buildBox.build[2]==1f || techBox.enable[7]==1) return true;
                else {resource.happy = Mathf.Max(0.8f,resource.happy-0.1f); return false;}
            case 5 :
                if(techBox.enable[4]==1) return true;
                else return false;
            case 6 :
                return false;
            case 7 :
                return false;
            case 8 :
                if(!eyeOn && saram.num[0]==1) {citizenBox.citizenKill(0,0,41);}
                return true;
            case 9 :
                if(!eyeOn) {
                    for(int i = 0; i < 3; i++) {
                        for(int j = 0; j < saram.num[i]; j++) {
                            saram.holy[i][j] *= 0.9f;
                        }
                    }
                }
                return true;
            case 10 :
                if(saram.num[0] ==1)
                    if(saram.char3[0][0] == 0 || saram.char3[0][0] == 5) return true;
                else {
                    hellLazy = true;
                    for(int i = 0; i < 3; i++) {
                        for(int j = 0; j < saram.num[i]; j++) {
                            saram.farming[i][j] *= 0.25f;
                            saram.fighting[i][j] *= 0.25f;
                        }
                    }
                    return true;
                }
                break;
            case 11 :
                enemyBox.GoWar(false);
                return false;
            case 12 :
                bigLose = true;
                return false;
            case 13 :
                return true;


        }
        return true;
    }
    public bool HellEventEndCondition()
    {
        if(seasonBox.hellEventEndTrigger) {
            return true;
        }
        switch(upcomingHell)
        {
            case 0 :
                return false;
            case 1 :
                return false;
            case 2 :
                return false;
            case 3 :
                return false; // need artificial rain trigger
            case 4 :
                return false; // need artificial sun trigger
            case 5 :
                return false;
            case 6 :
                return false; // need artificial sun trigger
            case 7 :
                return false; // need artificial rain trigger
            case 8 :
                return false;
            case 9 :
                return false;
            case 10 :
                return false;
            case 11 :
                return false;
            case 12 :
                return false;
            case 13 :
                return false;


        }
        return true;
    }
    public void ChangeHell(bool isSummer)
    {
        hellSprite.sprite = null;
        hellPrice += 5;
        hellPriceText.text = hellPrice.ToString();
        List<int> hellKind = new List<int>();
        for(int i = 0; i < hellSpriteBox.Length; i++)
        {
            if(i<=3) {
                if(isSummer) hellKind.Add(i);
            }
            else if(i<=6) {
                if(!isSummer) hellKind.Add(i);
            }
            else if(i==7)  {
                if(saram.num[0]==1) hellKind.Add(i);
            }
            else if(i==12) {
                if(isSummer && (enemyBox.fightDate==4 || mammothBox.HuntDate==4)) hellKind.Add(i);
                if(!isSummer && (enemyBox.fightDate==10 || mammothBox.HuntDate==10)) hellKind.Add(i);
            }
            else hellKind.Add(i);

        }
        int tempHappyDay = Random.Range(0,20);
        if(effectBox.enable[14] == 1 && tempHappyDay == 0) {
            upcomingHell = -1;
            hellSprite.sprite = hellHappyDaySprite;
        }
        else {
            upcomingHell = hellKind[Random.Range(0,hellKind.Count)];
        }

    }
}
