using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager: SavableObject
{
    public GameObject CardBox;
    public Sprite LockedSprite, UnlockedSprite;
    CardBox cardBox;
    
    public GameObject[] effectObjBox = new GameObject[]{null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
    SpriteRenderer[] effectStand = new SpriteRenderer[]{null,null,null,null,null,null,null,null};
    public int currentPage = 0; // 0 or 8

    /********** Save Data *********/
    public int[] enable = new int[]{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    /*******************************/
    public override void Load() {
        EffectSaveData data = SaveManager.Instance.LoadData.Effect;
        enable = data.enable;

        /* Enable 값에 따라 오브젝트 초기화 */
        for (int index = 0; index < enable.Length; index++)
        {
            if(enable[index] == 1)
            {
                objEnable(index);
            }
        }
    }
    public override void Save() {
        SaveManager.Instance.SaveData.Effect = new EffectSaveData(
            enable
        );
    }
    void Start()
    {        
        cardBox = CardBox.GetComponent<CardBox>();
        for(int i = 0; i < 8; i++)
            effectStand[i] = this.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();        
    }
    public void objEnable(int num)
    {
        effectObjBox[num] = Instantiate(CardBox.transform.GetChild(num).gameObject);
        effectObjBox[num].transform.SetParent(this.transform);
        effectObjBox[num].transform.position = this.transform.GetChild(num%8).position;
        if(num/8 != currentPage/8) 
        {
            effectObjBox[num].SetActive(false);
        }
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
                if(effectObjBox[i] != null) {
                    effectObjBox[i].SetActive(true);
                    effectStand[i-8].sprite = UnlockedSprite;
                }
                else effectStand[i-8].sprite = LockedSprite;
                    
            }
        }
        else if(currentPage == 8)
        {
            currentPage=0;
            for( int i = 0; i < 8; i++ )
            {
                if(effectObjBox[i] != null) {
                    effectObjBox[i].SetActive(true);
                    effectStand[i].sprite = UnlockedSprite;
                }
                else effectStand[i].sprite = LockedSprite;
            }
            for( int i = 8; i < 16; i++ )
            {
                if(effectObjBox[i] != null) effectObjBox[i].SetActive(false);
            }
        }
    }
    public void effectStandOpen(int num)
    {
        if(num-currentPage >= 0 && num-currentPage <= 7)
            effectStand[num-currentPage].sprite = UnlockedSprite;
    }

}
