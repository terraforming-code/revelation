using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechManager : MonoBehaviour
{
    public GameObject Resource, EnemyManager, CitizenManager, CardBox, buildManager;
    BuildingManager buildBox;
    Resource resource;
    Saram saram;
    EnemyManager enemyBox;
    CitizenManager citizenBox;
    CardBox cardBox;

    public int[] enable = new int[]{0,-1,-1,-1,-1,0,-1,0,-1,0,-1,-1,-1};

    bool ironAge = false;
    // Start is called before the first frame update
    void Start()
    {
        buildBox = buildManager.GetComponent<BuildingManager>();
        resource = Resource.GetComponent<Resource>();
        saram = Resource.GetComponent<Saram>();
        citizenBox = CitizenManager.GetComponent<CitizenManager>();
        enemyBox = EnemyManager.GetComponent<EnemyManager>();
        cardBox = CardBox.GetComponent<CardBox>();
    }

    public void techUnlock(int num) // should include sprite change
    {
        num-=100;
        enable[num] = 1;
        if(num<=6) {
            this.transform.GetChild(0).GetChild(num).GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite
            = CardBox.transform.GetChild(num+(100-cardBox.techStart)).GetComponent<SpriteRenderer>().sprite;
            
            buildManager.transform.GetChild(num).gameObject.SetActive(true);
            buildBox.build[num] = 0f;
        }
        else{
            this.transform.GetChild(1).GetChild(num-7).GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite
            = CardBox.transform.GetChild(num+(100-cardBox.techStart)).GetComponent<SpriteRenderer>().sprite;
        }
        switch(num)
        {
            case 0:
                enable[1]=0; enable[2]=0; break;
            case 2:
                enable[3]=0; enable[4]=0; break;
            case 5:
                enable[6]=0; break;
            case 7:
                resource.defense = 0.9f;
                enable[8]=0; break;
            case 8:
                resource.defense = 0.5f;
                break;
            case 9:
                resource.farmTech = 1.3f;
                enable[10]=0; enable[12]=0; break;
            case 10:
                resource.fightTech = 1.3f;
                enable[11]=0; break;
            case 11:
                resource.fightTech = 2f;
                break;
            case 12:
                resource.farmTech = 2f;
                break;

        }
    }
    public bool techCondition(int num)
    {
        num-=100;
        switch(num)
        {
            case 0:
                return (enemyBox.enemyLife >= 10);
            case 1:
                return true;  // heavy rain experience
            case 2:
                return (saram.num[0]+saram.num[1]+saram.num[2] >= 15); 
            case 3:
                if(saram.num[0] == 1) return (saram.char1[0][0] == 0);
                else return false; 
            case 4:
                return (resource.grain >= 100f); 
            case 5:
                return (citizenBox.diedCitizenNum >= 5); 
            case 6:
                float tempAvgHoly = 0f;
                for(int i = 0; i < 3; i++)
                    for(int j = 0; j < saram.num[i]; j++)
                        tempAvgHoly += saram.holy[i][j];
                return (tempAvgHoly/(saram.num[0]+saram.num[1]+saram.num[2]) >= 0.7f); 
            case 7:
                return true;  // mammoth raid success
            case 8:
                return ironAge; 
            case 9:
                return resource.poongzak; 
            case 10:
                return (enemyBox.fightCounter >= 10); 
            case 11:
                return ironAge; 
            case 12:
                return (saram.num[0]+saram.num[1]+saram.num[2] >= 10); 
        }
        return false;
    }

}

