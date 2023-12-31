using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager: SavableObject
{
    public GameObject SeasonPivot, CardBox, EffectManager, InvenManager, Resource, TechManager, HellManager;
    public Sprite EffectStand, InvenStand, TechStand;
    HellManager hellBox;
    TechManager techBox;
    SeasonManager seasonManager;
    CardBox cardBox;
    EffectManager effectBox;
    InvenManager invenBox;
    Resource resource;

    int cardAmount;

    // int shop1 = -1, shop2 = -1, shop3 = -1, shop4 = -1;
    SpriteRenderer shop1Image, shop2Image, shop3Image, shop4Image;
    SpriteRenderer shop1Stand, shop2Stand, shop3Stand, shop4Stand;
    TextMeshPro shop1Price, shop2Price, shop3Price, shop4Price, shopRerollPriceText;

    List<SpriteRenderer> shopImages = new List<SpriteRenderer>();
    List<SpriteRenderer> shopStands = new List<SpriteRenderer>();
    List<TextMeshPro> shopPriceTexts = new List<TextMeshPro>();
    // private bool prevIsHellRevealed = false;
    private int[] prevShops = {-2, -2, -2, -2};

    /********** Save Data *********/
    private int shopRerollPrice = 10; // base value changes at ChangeCard()
    private bool isHellRevealed = false;
    private int[] shops = {-1, -1, -1, -1};
    bool waitspring;
    /*******************************/
    public override void Load() {
        ShopSaveData data = SaveManager.Instance.LoadData.Shop;

        shopRerollPrice = data.shopRerollPrice;
        isHellRevealed = data.isHellRevealed;
        shops = data.shops;
        waitspring = data.waitspring;
        
        /* Arrange Shops */
        if (isHellRevealed){
            RevealHellCard();
        }
        Arrange();        
    }
    public override void Save() {
        SaveManager.Instance.SaveData.Shop = new ShopSaveData(
            shopRerollPrice,
            isHellRevealed,
            shops,
            waitspring
        );
    }
    void Awake()
    {
        /* 게임 오브젝트 연결 */
        hellBox = HellManager.GetComponent<HellManager>();
        techBox = TechManager.GetComponent<TechManager>();
        seasonManager = SeasonPivot.GetComponent<SeasonManager>();
        cardBox = CardBox.GetComponent<CardBox>();
        resource = Resource.GetComponent<Resource>();
        effectBox = EffectManager.GetComponent<EffectManager>();
        invenBox = InvenManager.GetComponent<InvenManager>();

        cardAmount = cardBox.price.Length;

        shopRerollPriceText = transform.Find("ShopReroll").GetChild(0).gameObject.GetComponent<TextMeshPro>();
        transform.Find("ShopReroll").Find("RerollButton").Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(HandleClickShopRerollButton); /* Reroll 버튼과 Handler Method 연결 */
        transform.Find("Body").Find("ShopHell").Find("BuyButton").Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(HandleClickCardHellBuyButton); /* HellBuy 버튼과 Handler Method 연결 */
        
        /* ShopImage, ShopStand, ShopPriceText, BuyButton */
        int shopCount = transform.Find("Body").Find("Shops").childCount;
        for (int i = 0; i < shopCount; i++)
        {
            Transform shop = transform.Find("Body").Find("Shops").GetChild(i);
            shopImages.Add(shop.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>());
            shopStands.Add(shop.GetChild(0).gameObject.GetComponent<SpriteRenderer>());
            shopPriceTexts.Add(shop.GetChild(1).gameObject.GetComponent<TextMeshPro>());
            int index = i;
            shop.Find("BuyButton").Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(()=>HandleClickCardBuyButton(index));/* Buy 버튼과 Handler Method 연결 */
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        // {
        //     Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        //     if(hit.collider != null)
        //     {
        //         GameObject click_obj = hit.transform.gameObject;
        //         if(click_obj.name == "ShopRerollButton") {
        //             resource.money -= shopRerollPrice;
        //             shopRerollPrice *= 3;// increase scale
        //             ChangeCard(true);
        //         }
        //         if(click_obj.name == "CardHellBuyButton" && hellBox.upcomingHell != -1 && hellBox.hellSprite.sprite == null) {
        //             if(resource.money >= hellBox.hellPrice) {
        //                 resource.money -= hellBox.hellPrice;
        //                 hellBox.hellSprite.sprite = hellBox.hellSpriteBox[hellBox.upcomingHell];
        //                 hellBox.hellPriceText.text = "";
        //             }
        //         }
        //         if(click_obj.name == "Card1BuyButton" && shop1 != -1) {
        //             if(resource.money >= cardBox.price[shop1]) {
        //                 if(BuyCard(shop1)) {
        //                     resource.money -= cardBox.price[shop1];
        //                     shop1Image.sprite = null;
        //                     shop1Stand.sprite = null;
        //                     shop1Price.text = "";
        //                     shop1 = -1;
        //                 }
        //             }
        //         }
        //         if(click_obj.name == "Card2BuyButton" && shop2 != -1) {
        //             if(resource.money >= cardBox.price[shop2]) {
        //                 if(BuyCard(shop2)) {
        //                     resource.money -= cardBox.price[shop2];
        //                     shop2Image.sprite = null;
        //                     shop2Stand.sprite = null;
        //                     shop2Price.text = "";
        //                     shop2 = -1;
        //                 }
        //             }
        //         }
        //         if(click_obj.name == "Card3BuyButton" && shop3 != -1) {
        //             if(resource.money >= cardBox.price[shop3]) {
        //                 if(BuyCard(shop3)) {
        //                     resource.money -= cardBox.price[shop3];
        //                     shop3Image.sprite = null;
        //                     shop3Stand.sprite = null;
        //                     shop3Price.text = "";
        //                     shop3 = -1;
        //                 }
        //             }
        //         }
        //         if(click_obj.name == "Card4BuyButton" && shop4 != -1) {
        //             if(resource.money >= cardBox.price[shop4]) {
        //                 if(BuyCard(shop4)) {
        //                     resource.money -= cardBox.price[shop4];
        //                     shop4Image.sprite = null;
        //                     shop4Stand.sprite = null;
        //                     shop4Price.text = "";
        //                     shop4 = -1;
        //                 }
        //             }
        //         }
        //     }
        // }
        if(seasonManager.season > 0 && seasonManager.season < 2 && waitspring)
        {
            waitspring = false;
            hellBox.ChangeHell(true);
            ChangeCard();
        }
        if(seasonManager.season > 2 && !waitspring)
        {
            waitspring = true;
            hellBox.ChangeHell(false);
            ChangeCard();
        }
    }

    public void HandleClickShopRerollButton(){
        resource.money -= shopRerollPrice;
        shopRerollPrice *= 3;// increase scale
        ChangeCard(true);
    }
    public void HandleClickCardHellBuyButton(){
        if(resource.money >= hellBox.hellPrice && hellBox.upcomingHell != -1 && hellBox.hellSprite.sprite == null) {
            resource.money -= hellBox.hellPrice;
            isHellRevealed = true;
            RevealHellCard();
        }
                
    }
    public void HandleClickCardBuyButton(int i) {
        Debug.Log("ShopManager: HandleClickCardBuyButton: i=" + i);
        if(shops[i] != -1 && resource.money >= cardBox.price[shops[i]] && BuyCard(shops[i])) {
            resource.money -= cardBox.price[shops[i]];
            shops[i] = -1;
            Arrange();
        }        
    }
    void Arrange() {
        for (int i = 0; i < shops.Length; i++){
            Debug.Log($"Arrange: loop: i={i} shops[i]={shops[i]}");
            if (shops[i] != prevShops[i]){
                if (shops[i] == -1){
                    shopImages[i].sprite = null;
                    shopStands[i].sprite = null;
                    shopPriceTexts[i].text = "";
                }
                else {
                    if(shops[i]<16) {
                        shopStands[i].sprite = EffectStand;
                    }
                    else if(shops[i]<100-cardBox.techStart) {
                        shopStands[i].sprite = InvenStand;
                    }
                    else {
                        shopStands[i].sprite = TechStand;
                    }
                    transform.Find("Body").Find("Shops").GetChild(i).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
                    shopImages[i].sprite = cardBox.transform.GetChild(shops[i]).GetComponent<SpriteRenderer>().sprite;
                    shopPriceTexts[i].text = cardBox.price[shops[i]].ToString();
                }
            }
        }
        shopRerollPriceText.text = shopRerollPrice.ToString();
        prevShops = (int[])shops.Clone();      
    }

    void RevealHellCard()
    {
        hellBox.hellSprite.sprite = hellBox.hellSpriteBox[hellBox.upcomingHell];
        hellBox.hellPriceText.text = "";
    }
    void ChangeCard(bool rerolling = false)
    {
        if(!rerolling) {
            shopRerollPrice = 10;
        }
        List<int> cardNow = new List<int>();
        
        int tempValue;
        for (int i = 1; i < 16; i++) {
            if(effectBox.enable[i]!=1) cardNow.Add(i);
        }
        if(cardNow.Count == 0) cardNow.Add(0);
        for(int i = 16; i < 100-cardBox.techStart; i++) {
            cardNow.Add(i);
        }
        for(int i = 0; i < 13; i++) {
            if(techBox.enable[i]==0) cardNow.Add(100-cardBox.techStart+i);
        }
        //Debug.Log($"Pool of ChangeCard End : {cardNow[cardNow.Count-1]}");

        for(int i=0; i < shops.Length; i++) {
            tempValue = Random.Range(0, cardNow.Count);
            shops[i] = cardNow[tempValue];
            if(shops[i] < 16) {
                cardNow.RemoveAt(tempValue);
            }
            else if (shops[i] >= 100-cardBox.techStart) {
                cardNow.RemoveAt(tempValue);
            }
            // shopImages[i].sprite = cardBox.transform.GetChild(shops[i]).GetComponent<SpriteRenderer>().sprite;
            // shopPriceTexts[i].text = cardBox.price[shops[i]].ToString();
        }
        Arrange();
        // tempValue = Random.Range(0,cardNow.Count);
        // shop1 = cardNow[tempValue];
        // if(shop1<16) {
        //     cardNow.RemoveAt(tempValue);
        //     shop1Stand.sprite = EffectStand;
        //     transform.GetChild(1).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        // }
        // else if(shop1<100-cardBox.techStart) {
        //     shop1Stand.sprite = InvenStand;
        //     transform.GetChild(1).GetChild(0).GetChild(0).localPosition = new Vector3(0,0.24f,0);
        // }
        // else {
        //     cardNow.RemoveAt(tempValue);
        //     shop1Stand.sprite = TechStand;
        //     transform.GetChild(1).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        // }
        // shop1Image.sprite = cardBox.transform.GetChild(shop1).GetComponent<SpriteRenderer>().sprite;


        // tempValue = Random.Range(0,cardNow.Count);
        // shop2 = cardNow[tempValue];
        // if(shop2<16) {
        //     cardNow.RemoveAt(tempValue);
        //     shop2Stand.sprite = EffectStand;
        //     transform.GetChild(2).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        // }
        // else if(shop2<100-cardBox.techStart) {
        //     shop2Stand.sprite = InvenStand;
        //     transform.GetChild(2).GetChild(0).GetChild(0).localPosition = new Vector3(0,0.24f,0);
        // }
        // else {
        //     cardNow.RemoveAt(tempValue);
        //     shop2Stand.sprite = TechStand;
        //     transform.GetChild(2).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        // }
        // shop2Image.sprite = cardBox.transform.GetChild(shop2).GetComponent<SpriteRenderer>().sprite;
        // tempValue = Random.Range(0,cardNow.Count);
        // shop3 = cardNow[tempValue];
        // if(shop3<16) {
        //     cardNow.RemoveAt(tempValue);
        //     shop3Stand.sprite = EffectStand;
        //     transform.GetChild(3).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        // }
        // else if(shop3<100-cardBox.techStart) {
        //     shop3Stand.sprite = InvenStand;
        //     transform.GetChild(3).GetChild(0).GetChild(0).localPosition = new Vector3(0,0.24f,0);
        // }
        // else {
        //     cardNow.RemoveAt(tempValue);
        //     shop3Stand.sprite = TechStand;
        //     transform.GetChild(3).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        // }
        // shop3Image.sprite = cardBox.transform.GetChild(shop3).GetComponent<SpriteRenderer>().sprite;
        // tempValue = Random.Range(0,cardNow.Count);
        // shop4 = cardNow[tempValue];
        // if(shop4<16) {
        //     cardNow.RemoveAt(tempValue);
        //     shop4Stand.sprite = EffectStand;
        //     transform.GetChild(4).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        // }
        // else if(shop4<100-cardBox.techStart) {
        //     shop4Stand.sprite = InvenStand;
        //     transform.GetChild(4).GetChild(0).GetChild(0).localPosition = new Vector3(0,0.24f,0);
        // }
        // else {
        //     cardNow.RemoveAt(tempValue);
        //     shop4Stand.sprite = TechStand;
        //     transform.GetChild(4).GetChild(0).GetChild(0).localPosition = new Vector3(0,0,0);
        // }
        // shop4Image.sprite = cardBox.transform.GetChild(shop4).GetComponent<SpriteRenderer>().sprite;


        // shop1Price.text = cardBox.price[shop1].ToString();
        // shop2Price.text = cardBox.price[shop2].ToString();
        // shop3Price.text = cardBox.price[shop3].ToString();
        // shop4Price.text = cardBox.price[shop4].ToString();
    }
    bool BuyCard(int num)
    {
        if(cardBox.type[num] == 0)
        {
            effectBox.enable[num] = 1;
            effectBox.objEnable(num);
            effectBox.effectStandOpen(num);
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
        transform.Find("Body").Find("Shops").GetChild(3).gameObject.SetActive(true);
        int cnt = 0;
        for(float y = 1.9f; y>=-2f; y-=3.41f)
        {
            
            for(float x = -2.14f; x<=6f; x+=5.23f)
            {
                //Debug.Log($"shop{cnt}Expanding : {x} {y}");
                //transform.GetChild(cnt).GetChild(0).localPosition = new Vector3(x,y,0f);
                transform.Find("Body").Find("Shops").GetChild(cnt).GetChild(1).localPosition = new Vector3(x+2.84f,y+0.4f,0f);
                transform.Find("Body").Find("Shops").GetChild(cnt).GetChild(2).localPosition = new Vector3(x+2.64f,y-1.1f,0f);
                transform.Find("Body").Find("Shops").GetChild(cnt).GetChild(0).localPosition = new Vector3(x,y-0.24f,0f);
                transform.Find("Body").Find("Shops").GetChild(cnt).GetChild(3).localPosition = new Vector3(x+2.64f,y+0.4f,0f);
                cnt++;
            }
        }
    }
}