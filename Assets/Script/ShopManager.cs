using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject SeasonPivot, CardBox, EffectManager, InvenManager, Resource;

    SeasonManager seasonManager;
    CardBox cardBox;
    EffectManager effectBox;
    InvenManager invenBox;
    Resource resource;

    int cardAmount = 5;

    int shop1 = -1, shop2 = -1, shop3 = -1;
    GameObject shop1obj, shop2obj, shop3obj;

    bool waitspring;
    // Start is called before the first frame update
    void Start()
    {
        seasonManager = SeasonPivot.GetComponent<SeasonManager>();
        cardBox = CardBox.GetComponent<CardBox>();
        resource = Resource.GetComponent<Resource>();
        effectBox = EffectManager.GetComponent<EffectManager>();
        invenBox = InvenManager.GetComponent<InvenManager>();
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
                            Destroy(shop1obj);
                            shop1 = -1;
                        }
                    }
                }
                if(click_obj.name == "Card2BuyButton" && shop2 != -1) {
                    if(resource.money >= cardBox.price[shop2]) {
                        if(BuyCard(shop2)) {
                            resource.money -= cardBox.price[shop2];
                            Destroy(shop2obj);
                            shop2 = -1;
                        }
                    }
                }
                if(click_obj.name == "Card3BuyButton" && shop3 != -1) {
                    if(resource.money >= cardBox.price[shop3]) {
                        if(BuyCard(shop3)) {
                            resource.money -= cardBox.price[shop3];
                            Destroy(shop3obj);
                            shop3 = -1;
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
        if(shop1 != -1) Destroy(shop1obj);
        if(shop2 != -1) Destroy(shop2obj);
        if(shop3 != -1) Destroy(shop3obj);
        shop1 = Random.Range(0,cardAmount);
        shop2 = Random.Range(0,cardAmount-1);
        shop3 = Random.Range(0,cardAmount-2);
        if(shop2>=shop1) shop2++;
        if(shop3>=shop1) shop3++;
        if(shop3>=shop2) shop3++;
        for (int i = 0; i < 16; i++) {
            if(effectBox.enable[i]==1) {
                if(shop1>=i) shop1++;
                if(shop2>=i) shop2++;
                if(shop3>=i) shop3++;
            }
        }
        Debug.Log($"{shop1} {shop2} {shop3}");
        shop1obj = Instantiate(CardBox.transform.GetChild(shop1).gameObject);
        shop1obj.transform.SetParent(this.transform);
        shop1obj.transform.position = this.transform.GetChild(1).position;
        shop2obj = Instantiate(CardBox.transform.GetChild(shop2).gameObject);
        shop2obj.transform.SetParent(this.transform);
        shop2obj.transform.position = this.transform.GetChild(2).position;
        shop3obj = Instantiate(CardBox.transform.GetChild(shop3).gameObject);
        shop3obj.transform.SetParent(this.transform);
        shop3obj.transform.position = this.transform.GetChild(3).position;
    }
    bool BuyCard(int num)
    {
        if(cardBox.type[num] == 0)
        {
            effectBox.enable[num] = 1;
            effectBox.objEnable(num);
            cardAmount--;
            return true;
        }
        else if(cardBox.type[num] == 1)
        {
            if(invenBox.invenNumBox.Count <= 6)
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
}
