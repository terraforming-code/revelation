using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RevelManager : MonoBehaviour
{
    public GameObject invenWindow, effectWindow, Resource, CardBox, messageManager;
    InvenManager invenBox;
    EffectManager effectBox;
    Resource resource;
    Saram saram;
    MessageManager messageBox;
    CardBox cards;
    GameObject MegaphoneButton;
    int pivot;
    public bool reveling = false;
    public int revelNum;
    public GameObject revelObj;

    float revelPercent;
    TextMeshPro revelPercentText;
    // Start is called before the first frame update
    void Awake()
    {
        invenBox = invenWindow.GetComponent<InvenManager>();
        effectBox = effectWindow.GetComponent<EffectManager>();
        resource = Resource.GetComponent<Resource>();
        saram = Resource.GetComponent<Saram>();
        cards = CardBox.GetComponent<CardBox>();
        messageBox = messageManager.GetComponent<MessageManager>();
        MegaphoneButton = this.transform.GetChild(3).gameObject;
        revelPercentText = this.transform.GetChild(4).gameObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if(hit.collider != null)
            {
                GameObject click_obj = hit.transform.gameObject;
                if(click_obj.name == "RevelMegaphone") {
                    resource.money--; // * megaphone price *
                    revelPercent = Mathf.Min(revelPercent+0.1f,1f); // *megaphone effect*
                    MegaphoneButton.SetActive(false);
                }
                if(click_obj.name == "RevelConfirm") {
                    if(Random.Range(0f,1f) <= revelPercent) // Successed
                    {
                        cards.skill(invenBox.invenNumBox[pivot]);
                        saram.HolyAdd(0.05f);
                        messageBox.messageAdd("Revealed");
                    }
                    else messageBox.messageAdd("Unrevealed");

                    invenBox.invenNumBox.RemoveAt(pivot);
                    invenBox.invenRearrange();
                    reveling = false;
                    if(effectBox.enable[3] == 1) MegaphoneButton.SetActive(true);
                    this.gameObject.SetActive(false);
                }
                if(click_obj.name == "RevelCancel") {
                    reveling = false;
                    if(effectBox.enable[3] == 1) MegaphoneButton.SetActive(true);
                    this.gameObject.SetActive(false);                   
                }
            }
        }
        
    }
    public void RevelOpen(int num)
    {
        
        pivot = num;

        reveling = true;
        revelNum = invenBox.invenNumBox[num];
        revelPercent = saram.holy[0][0];
        revelPercentText.text = Mathf.Round(revelPercent*100).ToString()+"%   "+Mathf.Round(100-revelPercent*100).ToString()+"%";
        revelObj.GetComponent<SpriteRenderer>().sprite = invenBox.invenBlock[num].sprite;
        //revelObj = Instantiate(invenBox.invenObjBox[num]);
        
    }
}
