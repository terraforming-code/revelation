using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject CardBox;
    CardBox cardBox;
    
    public int[] enable = new int[]{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    public GameObject[] effectObjBox = new GameObject[]{null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
    // Start is called before the first frame update
    void Start()
    {
        
        cardBox = CardBox.GetComponent<CardBox>();
        
    }
    public void objEnable(int num)
    {
        effectObjBox[num] = Instantiate(CardBox.transform.GetChild(num).gameObject);
        effectObjBox[num].transform.SetParent(this.transform);
        effectObjBox[num].transform.position = this.transform.GetChild(num).position;
        enable[num] = 1;
        cardBox.skill(num);
    }

}
