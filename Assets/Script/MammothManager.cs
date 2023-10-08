using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MammothManager : SavableObject
{
    public GameObject Resource, citizenManager, EnemyManager, hellManager;
    HellManager hellBox;
    Resource resource;
    Saram saram;
    EnemyManager enemyBox;
    CitizenManager citizenBox;

    public GameObject SeasonTab, SeasonBarMammoth;
    GameObject HuntDateObj;
    TextMeshPro mammothPowerText, mammothFoodText;


    /********** Save Data *********/
    public int HuntDate = -1;
    public int huntedExperience = 0;
    public float mammothPower = 10;
    public float mammothFood = 120f;
    /*******************************/
    public override void CreateNew()
    {
        MammothNew();        
    }
    public override void Load() {
        MammothSaveData data = SaveManager.Instance.LoadData.Mammoth;

        HuntDate = data.HuntDate;
        huntedExperience = data.huntedExperience;
        mammothPower = data.mammothPower;
        mammothFood = data.mammothFood;
        
        /* Arrange Mammoths */
        MammothObjRearrange();        
    }
    public override void Save() {
        SaveManager.Instance.SaveData.Mammoth = new MammothSaveData(
            HuntDate,
            huntedExperience,
            mammothPower,
            mammothFood
        );
    }
    void Awake()
    {
        hellBox = hellManager.GetComponent<HellManager>();
        resource = Resource.GetComponent<Resource>();
        saram = Resource.GetComponent<Saram>();
        citizenBox = citizenManager.GetComponent<CitizenManager>();
        enemyBox = EnemyManager.GetComponent<EnemyManager>();

        mammothPowerText = this.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        mammothFoodText = this.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
    }
    public void MammothNew()
    {
        mammothPower = 10f;
        mammothFood = 120f;
        for(int i = 0; i < huntedExperience; i++)
        {
            mammothPower *= 1.2f;
            mammothFood *= 1.2f;
        }
        MammothObjRearrange();
    }
    public void GoHunt(bool Attacked)
    {
        float realPower = (saram.num[0]==1? 1.0f : 0.8f) * resource.power;
        
        enemyBox.fightCounter++;
        if(realPower+0.1f >= mammothPower && !hellBox.bigLose)
        {
            saram.HolyAdd(0.1f);
            resource.happy = Mathf.Min(resource.happy+0.1f,1.2f);
            resource.food += mammothFood;
            huntedExperience++;
            MammothNew();
        }
        else
        {
            float cha = mammothPower * resource.defense * (saram.num[0]==1? (saram.char3[0][0]==5? 2f : 1f) : 1f)  * (hellBox.bigLose? 2f : 1f);
            while(saram.num[2] > 0)
            {
                if(cha < 1) break;
                citizenBox.citizenKill(2,0,3);
                cha --;
            }
            mammothPower -= realPower;
        }
        hellBox.bigLose = false;
        citizenBox.Arrange();

    }
    public void MammothObjRearrange()
    {
        if(HuntDateObj != null) {Destroy(HuntDateObj); HuntDateObj=null;}
        if(HuntDate != -1)
        {
            HuntDateObj = Instantiate(SeasonBarMammoth);
            HuntDateObj.transform.parent = SeasonTab.transform;
            HuntDateObj.transform.localPosition = new Vector3(HuntDate/12f*7f-3.5f,0,0);
        }
        mammothPowerText.text = mammothPower.ToString();
        mammothFoodText.text = mammothFood.ToString();
    }
}
