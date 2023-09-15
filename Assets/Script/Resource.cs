using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resource : MonoBehaviour
{
    public GameObject seasonManager, citizenManager;
    SeasonManager seasonBox;
    Saram saram;
    CitizenManager citizenBox;

    TextMeshPro moneyText, foodText, powerText;
    public int money = 30;
    public float food = 1f, power = 1f, love = 0f;

    int seasonEat = 0;
    bool waitNewSeason = true;
    void Start()
    {
        seasonBox = seasonManager.GetComponent<SeasonManager>();
        citizenBox = citizenManager.GetComponent<CitizenManager>();
        saram = GetComponent<Saram>();
        
        moneyText = this.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        foodText = this.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
        powerText = this.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
    }
    void Update()
    {
        moneyText.text = money.ToString();
        foodText.text = ((int)food).ToString();
        powerText.text = ((int)power).ToString();
        if((!waitNewSeason && seasonEat % 12 < seasonBox.season * 3) || (waitNewSeason && seasonBox.season < 0.33))
        {
            if(seasonEat % 12 == 6) {
                for(int i = 0; i<saram.num[1]; i++)
                {
                    food += saram.farming[1][i];
                }
            }
            if(seasonEat % 12 == 0) {
                for(int i = 0; i<Mathf.Max(saram.num[1],saram.num[2]); i++)
                {
                    if(i<saram.num[1]) love += saram.love[1][i];
                    if(i<saram.num[2]) love += saram.love[2][i];
                }
                while(love > 2)
                {
                    love -= 2;
                    citizenBox.citizenAdd();
                }
            }
            for(int i = Mathf.Max(saram.num[1],saram.num[2])-1; i>=0; i--)
            {
                if(i<saram.num[1])
                {
                    if(food - saram.eating[1][i] >= 0) food -= saram.eating[1][i];
                    else citizenBox.citizenKill(1,i);

                    
                }
                if(i<saram.num[2])
                {
                    if(food - saram.eating[2][i] >= 0) food -= saram.eating[2][i];
                    else citizenBox.citizenKill(2,i);

                    
                }
            }
            citizenBox.citizenRearrange();
            seasonEat++;
            waitNewSeason = false;
            if(seasonEat % 12 == 0) waitNewSeason = true;

        }
    }
}