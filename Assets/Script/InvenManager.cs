using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InvenManager : MonoBehaviour
{
    public GameObject CardBox;
    public GameObject RevelWindow; 
    public GameObject EffectWindow;
    public GameObject Resource;
    CardBox cardBox;
    RevelManager RevelBox;
    EffectManager EffectBox;
    Saram saram;

    public int count = 0;
    public List<int> invenNumBox = new List<int>();
    public SpriteRenderer[] invenBlock = new SpriteRenderer[]{null,null,null,null,null,null,null,null};
    public int invenLimit = 6;
    int invenSelect = -1;
    GameObject invenSelectObj;

    
    // Start is called before the first frame update
    void Start()
    {
        invenSelectObj = this.transform.GetChild(8).gameObject; /* SelectedCard (GetChild(8)) */
        cardBox = CardBox.GetComponent<CardBox>();
        RevelBox = this.transform.GetChild(11).gameObject.GetComponent<RevelManager>(); /* RevelWindow (GetChild(11)) */
        EffectBox = EffectWindow.GetComponent<EffectManager>();
        saram = Resource.GetComponent<Saram>();
        for(int i = 0; i < 8; i++) {
            int index = i; /* ! AddListener를 위해 필요 */
            invenBlock[i] = this.transform.GetChild(i).GetChild(0).gameObject.GetComponent<SpriteRenderer>();    
            this.transform.GetChild(i).GetChild(1).Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(()=>HandleClickInvenButton(index)); /* Inven 버튼과 Handler Method 연결 */
        }
        this.transform.GetChild(9).Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(HandleClickRevelButton); /* Revel버튼 (GetChild(9)) 과 Handler Method 연결 */
        this.transform.GetChild(10).Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(HandleClickRemoveButton); /* Remove버튼 (GetChild(10)) 과 Handler Method 연결 */
    }
    void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        // {
        //     Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        //     if(hit.collider != null)
        //     {
        //         GameObject click_obj = hit.transform.gameObject;
        //         if(click_obj.name == "RevelButton" && invenSelect != -1 && !RevelBox.reveling) {
        //             if(saram.num[0] == 1) {
        //                 Debug.Log($"RevelOpen : {invenSelect} {invenNumBox[invenSelect]}");
        //                 RevelWindow.SetActive(true);

        //                 //if(EffectBox.enable[3] != 1) RevelWindow.transform.GetChild(3).gameObject.SetActive(false); // megaphone check
        //                 //else RevelWindow.transform.GetChild(3).gameObject.SetActive(true); // megaphone check

        //                 RevelBox.reveling = true;
        //                 RevelBox.RevelOpen(invenSelect);
        //                 invenSelect = -1;
        //                 invenSelectObj.SetActive(false);
        //             }
        //         }
        //         else if(!RevelBox.reveling) {
        //             int clickNumber = -1;
        //             switch(click_obj.name) {
        //                 case "InvenCard0" :
        //                     clickNumber = 1; break;
        //                 case "InvenCard1" :
        //                     clickNumber = 2; break;
        //                 case "InvenCard2" :
        //                     clickNumber = 3; break;
        //                 case "InvenCard3" :
        //                     clickNumber = 4; break;
        //                 case "InvenCard4" :
        //                     clickNumber = 5; break;
        //                 case "InvenCard5" :
        //                     clickNumber = 6; break;
        //                 case "InvenCard6" :
        //                     clickNumber = 7; break;
        //                 case "InvenCard7" :
        //                     clickNumber = 8; break;
        //             }
        //             if(clickNumber != -1 && invenNumBox.Count >= clickNumber) {
        //                 invenSelect = clickNumber-1;
        //                 invenSelectObj.SetActive(true);
        //                 invenSelectObj.transform.position = this.transform.GetChild(clickNumber-1).position;
        //             }
        //         }
        //     }
        // }
    }

    public void HandleClickRevelButton(){
        Debug.Log("HandleClickRevelButton ");
        if(invenSelect != -1 && !RevelBox.reveling){
            if(saram.num[0] == 1)
            {
                Debug.Log($"RevelOpen : {invenSelect} {invenNumBox[invenSelect]}");
                RevelWindow.SetActive(true);

                //if(EffectBox.enable[3] != 1) RevelWindow.transform.GetChild(3).gameObject.SetActive(false); // megaphone check
                //else RevelWindow.transform.GetChild(3).gameObject.SetActive(true); // megaphone check

                RevelBox.reveling = true;
                RevelBox.RevelOpen(invenSelect);
                invenSelect = -1;
                invenSelectObj.SetActive(false);
            }
            else
            {
                Debug.Log("There's no preist. To revel, you should promote one of the citizens as priest at citizen window.");
            }
        } 
    }

    public void HandleClickInvenButton(int clickNumber){
        Debug.Log("HandleClickInvenButton clickNumber=" + clickNumber);
        if(!RevelBox.reveling && invenNumBox.Count > clickNumber){
            invenSelect = clickNumber;
            invenSelectObj.SetActive(true);
            invenSelectObj.transform.position = this.transform.GetChild(clickNumber).position;
        }
    }

    public void HandleClickRemoveButton(){
        Debug.Log("HandleClickRemoveButton");
        if(invenSelect != -1 && !RevelBox.reveling) {
            invenNumBox.RemoveAt(invenSelect);
            invenRearrange();
            invenSelect = -1;
            invenSelectObj.SetActive(false);
        }
    }
    public void invenAdd(int num)
    {
        invenNumBox.Add(num);
        invenRearrange();
    }
    public void invenRearrange()
    {
        for(int i = 0; i < invenLimit; i++)
        {
            if(i < invenNumBox.Count) invenBlock[i].sprite = cardBox.transform.GetChild(invenNumBox[i]%cardBox.techStart).gameObject.GetComponent<SpriteRenderer>().sprite;
            else invenBlock[i].sprite = null;
        }
    }
    public void invenExtend()
    {
        invenLimit = 8;
        this.transform.GetChild(6).gameObject.SetActive(true);
        this.transform.GetChild(7).gameObject.SetActive(true);
        int cnt = 0;
        for(float y = 0.9f; y>=-2f; y-=2.7f)    
        {
            for(float x = -3.9f; x<=4f; x+=2.6f)
            {
                this.transform.GetChild(cnt).localPosition = new Vector3(x,y,0f);
                cnt++;
            }
        }
    }

}
