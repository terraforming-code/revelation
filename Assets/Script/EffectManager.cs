using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{


    public GameObject CardBox;
    CardBox cardBox;
    
    public int[] enable = new int[]{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    public GameObject[] effectObjBox = new GameObject[]{null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};


    public int currentPage = 0; // 0 or 8
    // Start is called before the first frame update
    void Start()
    {
        
        cardBox = CardBox.GetComponent<CardBox>();
        
    }
    public void objEnable(int num)
    {
        effectObjBox[num] = Instantiate(CardBox.transform.GetChild(num).gameObject);
        effectObjBox[num].transform.SetParent(this.transform);
        effectObjBox[num].transform.position = this.transform.GetChild(num%8).position;
        if(num/8 != currentPage/8) effectObjBox[num].SetActive(false);
        enable[num] = 1;
        cardBox.skill(num);
    }
    public void effectPageChange()
    {
        if(currentPage == 0)
        {
            currentPage=8;
            for( int i = 0; i < 8; i++ )
            {
                if(effectObjBox[i] != null) effectObjBox[i].SetActive(false);
            }
            for( int i = 8; i < 16; i++ )
            {
                if(effectObjBox[i] != null) effectObjBox[i].SetActive(true);
            }
        }
        else if(currentPage == 8)
        {
            currentPage=0;
            for( int i = 0; i < 8; i++ )
            {
                if(effectObjBox[i] != null) effectObjBox[i].SetActive(true);
            }
            for( int i = 8; i < 16; i++ )
            {
                if(effectObjBox[i] != null) effectObjBox[i].SetActive(false);
            }
        }
    }

}
