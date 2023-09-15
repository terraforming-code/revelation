using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBox : MonoBehaviour
{
    public GameObject TabBox;
    public GameObject objShopWindowExtra, objInvenWindowExtra;

    public int[] price = new int[]{1,2,3,4,5,6,7,8,9,10,11,12};
    public int[] type = new int[]{0,0,0,0,0,0,0,1,1,1,2,2}; // effect = 0, revel = 1, tech = 2
    public void skill(int i)
    {
        if(i == 1)
        {
            TabBoxClickManager tabBoxVar = TabBox.GetComponent<TabBoxClickManager>();
            objShopWindowExtra.SetActive(true);
            tabBoxVar.objShopWindow.SetActive(false);
            tabBoxVar.objShopWindow = objShopWindowExtra;
        }
        if(i == 2)
        {
            TabBoxClickManager tabBoxVar = TabBox.GetComponent<TabBoxClickManager>();
            objInvenWindowExtra.SetActive(true);
            tabBoxVar.objInvenWindow.SetActive(false);
            tabBoxVar.objInvenWindow = objInvenWindowExtra;
        }
    }
}
