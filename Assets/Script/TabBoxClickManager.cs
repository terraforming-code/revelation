using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabBoxClickManager : MonoBehaviour
{
    public GameObject objCitizenWindow, objShopWindow, objInvenWindow, objTechWindow, objEffectWindow;

    private GameObject openTab;

    private Dictionary<string, GameObject> buttonNameToTab; 

    EffectManager effectBox;
    private string openTabName = "";

    // Start is called before the first frame update
    void Start()
    {
        buttonNameToTab = new Dictionary<string, GameObject>(){
            {"CitizenButton", objCitizenWindow},
            {"ShopButton", objShopWindow},
            {"InvenButton", objInvenWindow},
            {"TechButton", objTechWindow},
            {"EffectButton", objEffectWindow},
        };

        foreach (Transform child in transform)
        {
            child.Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(()=>HandleClickTabButton(child.gameObject.name));
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
        //         if(openTabName == click_obj.name) {
        //             openTab.transform.position = new Vector3(0,10,0);
        //             openTabName = null;
        //         }
        //         else {
        //             switch(click_obj.name) {
        //                 case "CitizenButton" :
        //                     if(openTab != null) openTab.transform.position = new Vector3(0,10,0);
        //                     openTab = objCitizenWindow;
        //                     openTab.transform.position = new Vector3(0,0.25f,0);
        //                     openTabName = click_obj.name;
        //                     break;
        //                 case "ShopButton" :
        //                     if(openTab != null) openTab.transform.position = new Vector3(0,10,0);
        //                     openTab = objShopWindow;
        //                     openTab.transform.position = new Vector3(0,0.25f,0);
        //                     openTabName = click_obj.name;
        //                     break;
        //                 case "InvenButton" :
        //                     if(openTab != null) openTab.transform.position = new Vector3(0,10,0);
        //                     openTab = objInvenWindow;
        //                     openTab.transform.position = new Vector3(0,0.25f,0);
        //                     openTabName = click_obj.name;
        //                     break;
        //                 case "TechButton" :
        //                     if(openTab != null) openTab.transform.position = new Vector3(0,10,0);
        //                     openTab = objTechWindow;
        //                     openTab.transform.position = new Vector3(0,0.25f,0);
        //                     openTabName = click_obj.name;
        //                     break;
        //                 case "EffectButton" :
        //                     if(openTab != null) openTab.transform.position = new Vector3(0,10,0);
        //                     openTab = objEffectWindow;
        //                     openTab.transform.position = new Vector3(0,0.25f,0);
        //                     openTabName = click_obj.name;
        //                     break;
        //             }
        //         }
        //         Debug.Log(click_obj.name);
        //     }
        // }
    }

    public void HandleClickTabButton(string buttonName)
    {
        if(openTabName == buttonName){
            openTab.transform.position = new Vector3(0,10,0);
            openTab = null;
            openTabName ="";
        }
        else{
            if(openTab != null) openTab.transform.position = new Vector3(0,10,0);
            openTab = buttonNameToTab[buttonName];
            openTab.transform.position = new Vector3(0,0.25f,0);
            openTabName = buttonName;
        }
    }
}
