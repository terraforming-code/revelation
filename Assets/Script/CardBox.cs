using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBox : MonoBehaviour
{
    public GameObject ShopWindow;
    public GameObject objCitizenWindow, objInvenWindow;
    public GameObject PriestCharChangeWindow;
    public GameObject AdditionSkillTab;
    public GameObject Resource, enemyManager;
    InvenManager invenBox;
    ShopManager shopBox;
    TabBoxClickManager tabBoxVar;
    CitizenManager citizenBox;
    PriestCharChangeManager priestCharChangeBox;
    Resource resource;
    Saram saram;
    EnemyManager enemyBox;

    public int[] price = new int[]{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24};
    public int[] type = new int[]{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1}; // effect = 0, revel = 1, tech = 2
    public int techStart = 75;
    void Start()
    {
        invenBox = objInvenWindow.GetComponent<InvenManager>();
        shopBox = ShopWindow.GetComponent<ShopManager>();
        citizenBox = objCitizenWindow.GetComponent<CitizenManager>();
        priestCharChangeBox = PriestCharChangeWindow.GetComponent<PriestCharChangeManager>();
        resource = Resource.GetComponent<Resource>();
        saram = Resource.GetComponent<Saram>();
        enemyBox = enemyManager.GetComponent<EnemyManager>();
    }
    public void skill(int i)
    {
        if(i == 1)
        {
            shopBox.shopExtend();
        }
        if(i == 2)
        {
            invenBox.invenExtend();
        }
        if(i == 3)
        {
            objInvenWindow.transform.GetChild(11).GetChild(3).gameObject.SetActive(true);
        }
        if(i == 4)
        {
            //objCitizenWindow.transform.Find("CitizenGroups").GetChild(0).GetChild(6).gameObject.SetActive(true);
            //objCitizenWindow.transform.Find("CitizenGroups").GetChild(1).GetChild(6).gameObject.SetActive(true);
            //objCitizenWindow.transform.Find("CitizenGroups").GetChild(2).GetChild(6).gameObject.SetActive(true);
        }
        if(i == 5)
        {
            AdditionSkillTab.transform.GetChild(0).gameObject.SetActive(true);
        }
        if(i == 6)
        {
            AdditionSkillTab.transform.GetChild(1).gameObject.SetActive(true);
        }
        if(i == 7)
        {
            AdditionSkillTab.transform.GetChild(2).gameObject.SetActive(true);
        }
        if(i == 16)
        {
            citizenBox.AddCitizen();
        }
        if(i == 17)
        {
            resource.food += 10;
        }
        if(i == 18)
        {
            resource.moretax *= 1.2f;
        }
        if(i == 19)
        {
            if(saram.num[2]>0) citizenBox.citizenMove(2,Random.Range(0,saram.num[2]),1);
        }
        if(i == 20)
        {
            if(saram.num[1]>0) citizenBox.citizenMove(1,Random.Range(0,saram.num[1]),2);
        }
        if(i == 21)
        {
            PriestCharChangeWindow.SetActive(true);
            priestCharChangeBox.priestCharChangeOpen();
        }
        if(i == 23)
        {
            enemyBox.fightDate = -1;
            enemyBox.enemyObjRearrange();
        }
        if(i == 100)
        {

        }
        if(i == 101)
        {
            
        }
    }
}
