using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject SeasonPivot, CardBox, EffectManager, InvenManager, Resource, TechManager;
    public Sprite EffectStand, InvenStand, TechStand;

    TechManager techBox;
    SeasonManager seasonManager;
    CardBox cardBox;
    EffectManager effectBox;
    InvenManager invenBox;
    Resource resource;

    int cardAmount;

    int shop1 = -1, shop2 = -1, shop3 = -1, shop4 = -1;
    SpriteRenderer shop1Image, shop2Image, shop3Image, shop4Image;
    SpriteRenderer shop1Stand, shop2Stand, shop3Stand, shop4Stand;
    TextMeshPro shop1Price, shop2Price, shop3Price, shop4Price;

    bool waitspring;
    // Start is called before the first frame update
    void Start()
    {
        techBox = TechManager.GetComponent<TechManager>();
        seasonManager = SeasonPivot.GetComponent<SeasonManager>();
        cardBox = CardBox.GetComponent<CardBox>();
        resource = Resource.GetComponent<Resource>();
        effectBox = EffectManager.GetComponent<EffectManager>();
        invenBox = InvenManager.GetComponent<InvenManager>();

        cardAmount = cardBox.price.Length;
        shop1Image = this.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop2Image = this.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop3Image = this.transform.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop4Image = this.transform.GetChild(4).GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop1Stand = this.transform.GetChild(1).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop2Stand = this.transform.GetChild(2).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop3Stand = this.transform.GetChild(3).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        shop4Stand = this.transform.GetChild(4).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
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
                            shop1Stand.sprite = null;
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
                            shop2Stand.sprite = null;
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
                            shop3Stand.sprite = null;
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
                            shop4Stand.sprite = null;
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
        for(int i = 16; i < 100-cardBox.techStart; i++) {
            cardNow.Add(i);
        }
        for(int i = 0; i < 13; i++) {
            if(techBox.enable[i]==0) cardNow.Add(100-cardBox.techStart+i);
        }
        //Debug.Log($"Pool of ChangeCard End : {cardNow[cardNow.Count-1]}");
        tempValue = Random.Range(0,cardNow.Count);
        shop1 = cardNow[tempValue];
        if(shop1<16) {
            cardNow.RemoveAt(tempValue);
            shop1Stand.sprite = EffectStand;
            this.transform.GetChild(1).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        }
        else if(shop1<100-cardBox.techStart) {
            shop1Stand.sprite = InvenStand;
            this.transform.GetChild(1).GetChild(0).GetChild(0).localPosition = new Vector3(0,0.24f,0);
        }
        else {
            cardNow.RemoveAt(tempValue);
            shop1Stand.sprite = TechStand;
            this.transform.GetChild(1).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        }
        shop1Image.sprite = cardBox.transform.GetChild(shop1).GetComponent<SpriteRenderer>().sprite;
        tempValue = Random.Range(0,cardNow.Count);
        shop2 = cardNow[tempValue];
        if(shop2<16) {
            cardNow.RemoveAt(tempValue);
            shop2Stand.sprite = EffectStand;
            this.transform.GetChild(2).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        }
        else if(shop2<100-cardBox.techStart) {
            shop2Stand.sprite = InvenStand;
            this.transform.GetChild(2).GetChild(0).GetChild(0).localPosition = new Vector3(0,0.24f,0);
        }
        else {
            cardNow.RemoveAt(tempValue);
            shop2Stand.sprite = TechStand;
            this.transform.GetChild(2).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        }
        shop2Image.sprite = cardBox.transform.GetChild(shop2).GetComponent<SpriteRenderer>().sprite;
        tempValue = Random.Range(0,cardNow.Count);
        shop3 = cardNow[tempValue];
        if(shop3<16) {
            cardNow.RemoveAt(tempValue);
            shop3Stand.sprite = EffectStand;
            this.transform.GetChild(3).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        }
        else if(shop3<100-cardBox.techStart) {
            shop3Stand.sprite = InvenStand;
            this.transform.GetChild(3).GetChild(0).GetChild(0).localPosition = new Vector3(0,0.24f,0);
        }
        else {
            cardNow.RemoveAt(tempValue);
            shop3Stand.sprite = TechStand;
            this.transform.GetChild(3).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        }
        shop3Image.sprite = cardBox.transform.GetChild(shop3).GetComponent<SpriteRenderer>().sprite;
        tempValue = Random.Range(0,cardNow.Count);
        shop4 = cardNow[tempValue];
        if(shop4<16) {
            cardNow.RemoveAt(tempValue);
            shop4Stand.sprite = EffectStand;
            this.transform.GetChild(4).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        }
        else if(shop4<100-cardBox.techStart) {
            shop4Stand.sprite = InvenStand;
            this.transform.GetChild(4).GetChild(0).GetChild(0).localPosition = new Vector3(0,0.24f,0);
        }
        else {
            cardNow.RemoveAt(tempValue);
            shop4Stand.sprite = TechStand;
            this.transform.GetChild(4).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        }
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
        else if(cardBox.type[num] == 2)
        {
            if(invenBox.invenNumBox.Count <= invenBox.invenLimit)
            {
                invenBox.invenAdd(num+cardBox.techStart);
                techBox.enable[(num+cardBox.techStart)%100] = -1;
                return true;
            }
            else return false;
        }
        else return false;
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
                //this.transform.GetChild(cnt).GetChild(0).localPosition = new Vector3(x,y,0f);
                this.transform.GetChild(cnt).GetChild(1).localPosition = new Vector3(x+2.84f,y+0.4f,0f);
                this.transform.GetChild(cnt).GetChild(2).localPosition = new Vector3(x+2.64f,y-1.1f,0f);
                this.transform.GetChild(cnt).GetChild(0).localPosition = new Vector3(x,y-0.24f,0f);
                this.transform.GetChild(cnt).GetChild(3).localPosition = new Vector3(x+2.64f,y+0.4f,0f);
            }
        }
    }
}
