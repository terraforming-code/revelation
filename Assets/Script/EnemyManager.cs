using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : SavableObject
{
    public GameObject Resource, citizenManager;
    Resource resource;
    Saram saram;
    CitizenManager citizenBox;

    public GameObject SeasonTab, SeasonBarBear, hellManager;
    HellManager hellBox;
    GameObject fightDateObj;
    TextMeshPro enemyLifeText, enemyPowerText;

    /********** Save Data *********/
    public int enemyLife = 2;
    public int fightDate = -1;    
    public int fightCounter = 0;
    public float newenemyPower = 1f; 
    public float enemyPower = 2f;
    /*******************************/
    public override void Load() {
        EnemySaveData data = SaveManager.Instance.LoadData.Enemy;

        enemyLife = data.enemyLife;
        fightDate = data.fightDate;    
        fightCounter = data.fightCounter;
        newenemyPower = data.newenemyPower; 
        enemyPower = data.enemyPower;      
    }
    public override void Save() {
        SaveManager.Instance.SaveData.Enemy = new EnemySaveData(
            enemyLife,
            fightDate,    
            fightCounter,
            newenemyPower, 
            enemyPower   
        );
    }
    void Awake()
    {
        hellBox = hellManager.GetComponent<HellManager>();
        resource = Resource.GetComponent<Resource>();
        saram = Resource.GetComponent<Saram>();
        citizenBox = citizenManager.GetComponent<CitizenManager>();

        enemyLifeText = this.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        enemyPowerText = this.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();        
    }
    public void enemyGrow()
    {
        int newLife = enemyLife/2 + Random.Range(0,2);
        enemyLife += newLife;
        enemyPower += newLife * newenemyPower;
        newenemyPower += 0.1f;
    }

    public void GoWar(bool Attacked)
    {
        float realPower = (saram.num[0]==1? 1.0f : 0.8f) * resource.power;
        float cha;
        float enemyPowerPerMan = enemyPower/enemyLife;
        fightCounter++;
        if(realPower >= enemyPower && !hellBox.bigLose)
        {
            saram.HolyAdd(0.1f);
            resource.happy = Mathf.Min(resource.happy+0.1f,1.2f);
            cha = enemyPower / 2 * resource.defense * (saram.num[0]==1? (saram.char3[0][0]==5? 2f : 1f) : 1f);
            while(saram.num[2] > 0)
            {
                Debug.Log($"Died army left {cha}");
                if(cha < 1) break;
                citizenBox.citizenKill(2,0,3);
                cha --;
            }
            cha = realPower;
            while(enemyLife > 0)
            {
                Debug.Log($"Died enemy left {cha}");
                if(cha < 1) break;
                enemyLife--;
                enemyPower -= enemyPowerPerMan;
                cha --;
            }
        }
        else
        {
            saram.HolyAdd(-0.1f);
            cha = enemyPower * resource.defense * (saram.num[0]==1? (saram.char3[0][0]==5? 2f : 1f) : 1f) * (hellBox.bigLose? 2f : 1f);
            while(saram.num[2] > 0)
            {
                Debug.Log($"Died army left {cha}");
                if(cha < 1) break;
                citizenBox.citizenKill(2,0,3);
                cha --;
            }
            cha = realPower / 2 ;
            while(enemyLife > 0)
            {
                Debug.Log($"Died enemy left {cha}");
                if(cha < 1) break;
                enemyLife--;
                enemyPower -= enemyPowerPerMan;
                cha --;
            }  
        }        
        citizenBox.Arrange();
    }
    public void enemyObjRearrange()
    {
        if(fightDateObj != null) {Destroy(fightDateObj); fightDateObj=null;}
        if(fightDate != -1)
        {
            fightDateObj = Instantiate(SeasonBarBear);
            fightDateObj.transform.parent = SeasonTab.transform;
            fightDateObj.transform.localPosition = new Vector3(fightDate/12f*7f-3.5f,0,0);
        }
        enemyLifeText.text = enemyLife.ToString("F1");
        enemyPowerText.text = enemyPower.ToString("F1");
    }
}
