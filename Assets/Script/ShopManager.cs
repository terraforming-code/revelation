using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject SeasonPivot, CardBox, EffectManager, InvenManager, Resource;

    SeasonManager seasonManager;
    CardBox cardBox;
    EffectManager effectBox;
    InvenManager invenBox;
    Resource resource;

    int cardAmount;

    int shop1 = -1, shop2 = -1, shop3 = -1, shop4 = -1;
    SpriteRenderer shop1Image, shop2Image, shop3Image, shop4Image;
    TextMeshPro shop1Price, shop2Price, shop3Price, shop4Price;

    bool waitspring;
    // Start is called before the first frame update
    void Start()
    {
        seasonManager = SeasonPivot.GetComponent<SeasonManager>();
        cardBox = CardBox.GetComponent<CardBox>();
        resource = Resource.GetComponent<Resource>();
        effectBox = EffectManager.GetComponent<EffectManager>();
        invenBox = InvenManager.GetComponent<InvenManager>();

        cardAmount = 25;//cardBox.price.Length;
        shop1Image = this.transform.GetChild(1).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop2Image = this.transform.GetChild(2).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop3Image = this.transform.GetChild(3).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop4Image = this.transform.GetChild(4).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop1Price = this.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshPro>();
        shop2Price = this.transform.GetChild(2).GetChild(1).gameObject.GetComponent<TextMeshPro>();
        shop3Price = this.transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshPro>();
        shop4Price = this.transform.GetChild(4).GetChild(1).gameObject.GetComponent<TextMeshPro>();
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
                if(click_obj.name == "Card1BuyButton" && shop1 != -1) {
                    if(resource.money >= cardBox.price[shop1]) {
                        if(BuyCard(shop1)) {
                            resource.money -= cardBox.price[shop1];
                            shop1Image.sprite = null;
                            shop1Price.text = "";
                            shop1 = -1;
                        }
                    }
                }
                if(click_obj.name == "Card2BuyButton" && shop2 != -1) {
                    if(resource.money >= cardBox.price[shop2]) {
                        if(BuyCard(shop2)) {
                            resource.money -= cardBox.price[shop2];
                            shop2Image.sprite = null;
                            shop2Price.text = "";
                            shop2 = -1;
                        }
                    }
                }
                if(click_obj.name == "Card3BuyButton" && shop3 != -1) {
                    if(resource.money >= cardBox.price[shop3]) {
                        if(BuyCard(shop3)) {
                            resource.money -= cardBox.price[shop3];
                            shop3Image.sprite = null;
                            shop3Price.text = "";
                            shop3 = -1;
                        }
                    }
                }
                if(click_obj.name == "Card4BuyButton" && shop4 != -1) {
                    if(resource.money >= cardBox.price[shop4]) {
                        if(BuyCard(shop4)) {
                            resource.money -= cardBox.price[shop4];
                            shop4Image.sprite = null;
                            shop4Price.text = "";
                            shop4 = -1;
                        }
                    }
                }
            }
        }
        if(seasonManager.season > 0 && seasonManager.season < 2 && waitspring)
        {
            waitspring = false;
            ChangeCard();
        }
        if(seasonManager.season > 2 && !waitspring)
        {
            waitspring = true;
            ChangeCard();
        }
    }

    void ChangeCard()
    {
        List<int> cardNow = new List<int>();
        int tempValue;
        for (int i = 0; i < 16; i++) {
            if(effectBox.enable[i]!=1) cardNow.Add(i);
        }
        for(int i = 16; i < cardAmount; i++) {
            cardNow.Add(i);
        }
        //Debug.Log($"Pool of ChangeCard End : {cardNow[cardNow.Count-1]}");
        tempValue = 21; //Random.Range(0,cardNow.Count);
        shop1 = cardNow[tempValue];
        if(shop1>= 0 && shop1<16) cardNow.RemoveAt(tempValue);
        shop1Image.sprite = cardBox.transform.GetChild(shop1).GetComponent<SpriteRenderer>().sprite;
        tempValue = Random.Range(0,cardNow.Count);
        shop2 = cardNow[tempValue];
        if(shop2>= 0 && shop2<16) cardNow.RemoveAt(tempValue);
        shop2Image.sprite = cardBox.transform.GetChild(shop2).GetComponent<SpriteRenderer>().sprite;
        tempValue = Random.Range(0,cardNow.Count);
        shop3 = cardNow[tempValue];
        if(shop3>= 0 && shop3<16) cardNow.RemoveAt(tempValue);
        shop3Image.sprite = cardBox.transform.GetChild(shop3).GetComponent<SpriteRenderer>().sprite;
        tempValue = Random.Range(0,cardNow.Count);
        shop4 = cardNow[tempValue];
        if(shop4>= 0 && shop4<16) cardNow.RemoveAt(tempValue);
        shop4Image.sprite = cardBox.transform.GetChild(shop4).GetComponent<SpriteRenderer>().sprite;

        shop1Price.text = cardBox.price[shop1].ToString();
        shop2Price.text = cardBox.price[shop2].ToString();
        shop3Price.text = cardBox.price[shop3].ToString();
        shop4Price.text = cardBox.price[shop4].ToString();
    }
    bool BuyCard(int num)
    {
        if(cardBox.type[num] == 0)
        {
            effectBox.enable[num] = 1;
            effectBox.objEnable(num);
            return true;
        }
        else if(cardBox.type[num] == 1)
        {
            if(invenBox.invenNumBox.Count <= invenBox.invenLimit)
            {
                invenBox.invenAdd(num);
                return true;
            }
            else return false;
        }
        else
        {
            return false;
        }
    }
    public void shopExtend()
    {
        this.transform.GetChild(4).gameObject.SetActive(true);
        int cnt = 0;
        for(float y = 1.9f; y>=-2f; y-=3.41f)
        {
            
            for(float x = -2.14f; x<=6f; x+=5.23f)
            {
                cnt++;
                //Debug.Log($"shop{cnt}Expanding : {x} {y}");
                this.transform.GetChild(cnt).GetChild(0).localPosition = new Vector3(x,y,0f);
                this.transform.GetChild(cnt).GetChild(1).localPosition = new Vector3(x+2.84f,y+0.4f,0f);
                this.transform.GetChild(cnt).GetChild(2).localPosition = new Vector3(x+2.64f,y-1.1f,0f);
                this.transform.GetChild(cnt).GetChild(3).localPosition = new Vector3(x,y-0.24f,0f);
                this.transform.GetChild(cnt).GetChild(4).localPosition = new Vector3(x+2.64f,y+0.4f,0f);
            }
        }
    }
}
