using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resource : MonoBehaviour
{
    public GameObject seasonManager, citizenManager, messageManager, enemyManager, buildManager, mammothManager, hellManager, effectManager, techManager;
    public GameObject BGRainbow;
    HellManager hellBox;
    EffectManager effectBox;
    MammothManager mammothBox;
    BuildingManager buildBox;
    SeasonManager seasonBox;
    TechManager techBox;
    Saram saram;
    CitizenManager citizenBox;
    EnemyManager enemyBox;
    MessageManager messageBox;

    TextMeshPro moneyText, foodText, powerText, grainText;
    public int money = 30;
    public float food = 1f, power = 1f, love = 0f, grain = 0f, defense = 1f, farmTech = 1f, fightTech = 1f;
    public float happy = 0.9f;

    public float moretax = 1f;

    int seasonEat = 0;
    float foodLimit = 500f;
    bool waitNewSeason = true;
    
    public bool poongzak = false;

    float happyBeforeHell;
    void Start()
    {
        effectBox = effectManager.GetComponent<EffectManager>();
        hellBox = hellManager.GetComponent<HellManager>();
        mammothBox = mammothManager.GetComponent<MammothManager>();
        buildBox = buildManager.GetComponent<BuildingManager>();
        seasonBox = seasonManager.GetComponent<SeasonManager>();
        citizenBox = citizenManager.GetComponent<CitizenManager>();
        enemyBox = enemyManager.GetComponent<EnemyManager>();
        messageBox = messageManager.GetComponent<MessageManager>();
        techBox = techManager.GetComponent<TechManager>();
        saram = GetComponent<Saram>();
        
        moneyText = this.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        foodText = this.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
        powerText = this.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
        grainText = this.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>();
    }
    void Update()
    {
        moneyText.text = money.ToString();
        if(food > foodLimit && buildBox.build[4]!=1f) food = foodLimit; // FOOD LIMIT
        foodText.text = ((int)food).ToString(); 
        powerText.text = power.ToString("F1");
        grainText.text = ((int)grain).ToString();
        if((!waitNewSeason && seasonEat % 12 < seasonBox.season * 3) || (waitNewSeason && seasonBox.season < 0.33))
        {
            float tempPower = 0f;
            // farm process
            if(seasonEat % 12 == 6 || seasonEat % 12 == 0) {
                float taxPercent = 0f;
                if(saram.num[0] == 1) { // if priest exist
                    if(saram.char2[0][0] == 5) moretax = 0f; // anarchist
                    else if(saram.char2[0][0] == 2 || saram.char2[0][0] == 3) moretax *= 1.5f; // toxic holy priest
                    taxPercent = Mathf.Min(saram.holy[0][0]*moretax,1);
                    moretax = 1f;
                }
                money += (int)(grain * taxPercent);
                food += grain * (1 - taxPercent);
                messageBox.messageAdd(((int)(grain * taxPercent)).ToString()+" grains was sacrificed");
                messageBox.messageAdd((grain * (1 - taxPercent)).ToString()+" grains added by farming");
                float goodCrop = 0f;
                for(int i = 0; i < 3; i++)
                {
                    for(int j = 0; j < saram.num[i]; j++)
                    {
                        goodCrop += saram.eating[i][j];
                    }
                }
                if(goodCrop*12>=grain*taxPercent) {happy=Mathf.Min(1.2f,happy+0.1f); poongzak=true;}
                grain = 0f;
            }
            else {
                float godfarmer = 1f;
                if(saram.num[0] == 1) {
                    if(saram.char1[0][0] == -11) godfarmer = 1.5f;
                }
                for(int i = 0; i<saram.num[1]; i++)
                {
                    if(seasonEat % 12 <= 6) grain += saram.farming[1][i] * happy * farmTech * godfarmer;
                    grain += saram.farming[1][i] / 2f * happy * farmTech * godfarmer;
                }
                
            }

            // fanatic kill process
            if(saram.num[0] == 1){
                if(saram.char2[0][0] == 2)
                {
                    int minholyPivot = -1;
                    float minholyValue = 1.1f;
                    for(int i = 0; i<Mathf.Max(saram.num[1],saram.num[2]); i++)
                    {
                        if(i<saram.num[1]) {
                            if(saram.holy[1][i] < minholyValue) {minholyPivot = i+1000; minholyValue = saram.holy[1][i];}

                        }
                        if(i<saram.num[2]) {
                            if(saram.holy[2][i] < minholyValue) {minholyPivot = i+2000; minholyValue = saram.holy[2][i];}

                        }
                    }
                    citizenBox.citizenKill(minholyPivot/1000,minholyPivot%1000,40);
                }
            }
            
            
            // make life process (include enemy)
            if(seasonEat % 12 == 0) {
                enemyBox.enemyGrow();
                for(int i = 0; i<Mathf.Max(saram.num[1],saram.num[2]); i++)
                {
                    if(i<saram.num[1]) love += saram.love[1][i];
                    if(i<saram.num[2]) love += saram.love[2][i];
                }
                if(saram.num[0] == 1)
                {
                    if(saram.char3[0][0] == 4 && love>2) love = Mathf.Max(love*1.5f,love+2); 
                }
                while(love > 2)
                {
                    love -= 2;
                    citizenBox.citizenAdd();
                }
            }
            // eat process
            for(int i = Mathf.Max(saram.num[1],saram.num[2])-1; i>=0; i--)
            {
                if(i<saram.num[1])
                {
                    if(food - saram.eating[1][i] >= 0) food -= saram.eating[1][i];
                    else citizenBox.citizenKill(1,i,0);

                    
                }
                if(i<saram.num[2])
                {
                    if(food - saram.eating[2][i] >= 0) {
                        food -= saram.eating[2][i];
                        tempPower += saram.fighting[2][i];
                    }
                    else citizenBox.citizenKill(2,i,0);

                }
            }
            //power process
            if(saram.num[0] == 1) power = (saram.char3[0][0] == 3? 1.2f : 1)*tempPower * happy * fightTech * (saram.char1[0][0]==-10? 1.5f : 1);
            else power = tempPower * happy * fightTech;


            //enemy&mammoth fight process
            if(seasonEat % 3 == 1)
            {
                if(enemyBox.fightDate == seasonEat % 12)
                {
                    enemyBox.GoWar(true);
                    enemyBox.fightDate = -1;
                }
                else if(mammothBox.HuntDate == seasonEat % 12)
                {
                    mammothBox.GoHunt(true);
                    mammothBox.HuntDate = -1;
                }
                if(saram.num[0] == 1)
                {
                    if(saram.char1[0][0] == 5 || saram.char1[0][0] == 0)
                    {
                        enemyBox.fightDate = -1;
                    }
                    else if((saram.char3[0][0] == 0 || saram.char3[0][0] == 5) && enemyBox.fightDate == -1)
                    {
                        if(Random.Range(0,2) == 0) {enemyBox.fightDate = seasonEat % 12; }
                        //enemyBox.fightDate = seasonEat % 12; // for debugging
                    }
                    else if(enemyBox.fightDate == -1)
                    {
                        if(Random.Range(0,5) == 0) {enemyBox.fightDate = seasonEat % 12; }
                        //enemyBox.fightDate = seasonEat % 12; // for debugging
                    }

                    if(saram.char1[0][0] == 0)
                    {
                        mammothBox.HuntDate = -1;
                    }
                    else if((saram.char3[0][0] == 0) && mammothBox.HuntDate == -1 && enemyBox.fightDate != (seasonEat+6)%12)
                    {
                        if(Random.Range(0,2) == 0) {mammothBox.HuntDate = (seasonEat+6) % 12; }
                        mammothBox.HuntDate = (seasonEat+6) % 12; // for debugging
                    }
                    else if(mammothBox.HuntDate == -1 && enemyBox.fightDate != (seasonEat+6)%12)
                    {
                        if(Random.Range(0,5) == 0) {mammothBox.HuntDate = (seasonEat+6) % 12; }
                        mammothBox.HuntDate = (seasonEat+6) % 12; // for debugging
                    }
                }
                enemyBox.enemyObjRearrange();
                mammothBox.mammothObjRearrange();
            }

            //citizen rearrange
            citizenBox.citizenRearrange();

            //hell process + Rainbow
            if(seasonEat % 12 == 3 || seasonEat % 12 == 9 )
            {
                hellBox.guardHell = hellBox.HellGuardCondition();
            }
            else if(seasonEat % 12 == 4 || seasonEat % 12 == 10 )
            {
                if(hellBox.guardHell == false)
                {
                    if(Random.Range(0,6)==0 && effectBox.enable[13] == 1)
                    {
                        BGRainbow.SetActive(true);
                        happy = Mathf.Max(happy,happyBeforeHell);
                    }
                }
                hellBox.hellDieNum *= 1.1f;
                hellBox.guardHell = true;
                hellBox.bigLose = false;
                hellBox.hellDie = 0f;
                hellBox.hellDestroy = 0f;
                if(hellBox.hellLazy) {
                    hellBox.hellLazy = false;
                    for(int i = 0; i < 3; i++) {
                        for(int j = 0; j < saram.num[i]; j++) {
                            saram.farming[i][j] *= 4f;
                            saram.fighting[i][j] *= 4f;
                        }
                    }
                }
            }
            else if(seasonEat % 12 == 5 || seasonEat % 12 == 11)
            {
                BGRainbow.SetActive(false);
            }
            
            //ironAge Process by happiness
            if(techBox.ironAgeStart && happy > 1.19f && techBox.ironAgeComing < 12) {
                techBox.ironAgePlus();
            }

            seasonEat++;
            waitNewSeason = false;
            if(seasonEat % 12 == 0) waitNewSeason = true;

            // priest process (job change)
            if(saram.num[0] == 1)
            {
                int maxPivot = -1;
                float maxValue = 0f;
                if(saram.char1[0][0] == 0)
                {
                    // no job change   
                }
                else if(saram.char3[0][0] == 3)
                {
                    for(int i = 0;i < saram.num[1];i ++)
                    {
                        if(maxValue < saram.fighting[1][i]) {
                            maxPivot = i;
                            maxValue = saram.fighting[1][i];
                        }
                    }
                    if(Random.Range(0,saram.num[1]+saram.num[2]) < 2*saram.num[1]) citizenBox.citizenMove(1,maxPivot,2);
                }
                else if(saram.char1[0][0] == 5)
                {
                    for(int i = 0;i < saram.num[2];i ++)
                    {
                        if(maxValue < saram.farming[2][i]) {
                            maxPivot = i;
                            maxValue = saram.farming[2][i];
                        }
                    }
                    if(Random.Range(0,saram.num[1]+saram.num[2]) < 2*saram.num[2]) citizenBox.citizenMove(2,maxPivot,1);
                }
                else if(saram.char1[0][0] == 4 || saram.char3[0][0] == 5)
                {
                    if(Random.Range(0,saram.num[1]+saram.num[2]) < saram.num[1]) citizenBox.citizenMove(1,Random.Range(0,saram.num[1]),2);
                }
                else if(saram.char2[0][0] == 0)
                {
                    if(Random.Range(0,saram.num[1]+saram.num[2]) < saram.num[2]) citizenBox.citizenMove(2,Random.Range(0,saram.num[2]),1);
                }
            }

        }
    }
    
}