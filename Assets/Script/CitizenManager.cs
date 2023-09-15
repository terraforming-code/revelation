using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CitizenManager : MonoBehaviour
{
    public GameObject Resource, messageManager;
    Resource resource;
    Saram saram;
    MessageManager messageBox;

    GameObject upObj, downObj, tabPivotObj;
    int tabNum = 1;
    int[] citizenPivot = new int[]{0,0,0};


    // Start is called before the first frame update
    void Start()
    {
        upObj = this.transform.GetChild(3).gameObject;
        downObj = this.transform.GetChild(4).gameObject;
        tabPivotObj = this.transform.GetChild(5).gameObject;
        resource = Resource.GetComponent<Resource>();
        saram = Resource.GetComponent<Saram>();
        messageBox = messageManager.GetComponent<MessageManager>();
        
        // Add proto DB
        for(int i = 0; i<5; i++)
            citizenAdd();

        

        

        citizenRearrange();
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
                switch(click_obj.name) {
                    case "CitizenTabButton1" :
                        tabNum = 0; citizenRearrange();
                        tabPivotObj.transform.position = new Vector3(tabPivotObj.transform.position.x,hit.transform.position.y,0); break;
                    case "CitizenTabButton2" :
                        tabNum = 1; citizenRearrange(); 
                        tabPivotObj.transform.position = new Vector3(tabPivotObj.transform.position.x,hit.transform.position.y,0); break;
                    case "CitizenTabButton3" :
                        tabNum = 2; citizenRearrange();
                        tabPivotObj.transform.position = new Vector3(tabPivotObj.transform.position.x,hit.transform.position.y,0); break;
                    case "CitizenKill1" :
                        citizenKill(tabNum,citizenPivot[tabNum] + 0); citizenRearrange(); break;
                    case "CitizenKill2" :
                        citizenKill(tabNum,citizenPivot[tabNum] + 1); citizenRearrange(); break;
                    case "CitizenKill3" :
                        citizenKill(tabNum,citizenPivot[tabNum] + 2); citizenRearrange(); break;
                    case "CitizenUp" :
                        citizenPivot[tabNum]-=3; citizenRearrange(); break;
                    case "CitizenDown" :
                        citizenPivot[tabNum]+=3; citizenRearrange(); break;
                }
            }
        }
    }
    public void citizenAdd()
    {
        int job = Random.Range(0,2)+1;
        string new_nickname = saram.nicknameTag1[Random.Range(0,saram.nicknameTag1.Length)]+" "+saram.nicknameTag2[Random.Range(0,saram.nicknameTag2.Length)];
        saram.nickname[job].Add(new_nickname);
        
        saram.holy[job].Add((float)Random.Range(8,13) / 10.0f);
        saram.farming[job].Add((float)Random.Range(8,13) / 10.0f * 30f);
        saram.eating[job].Add((float)Random.Range(8,13) / 10.0f);
        saram.fighting[job].Add((float)Random.Range(8,13) / 10.0f);
        saram.love[job].Add((float)Random.Range(8,13) / 10.0f);
        saram.life[job].Add(Random.Range(1.0f,3.0f));

        saram.num[job]++;

        messageBox.messageAdd("New Live : "+new_nickname);


    }
    public void citizenKill(int job,int killpivot)
    {
        saram.nickname[job].RemoveAt( killpivot );
        saram.holy[job].RemoveAt( killpivot );
        saram.farming[job].RemoveAt( killpivot );
        saram.eating[job].RemoveAt( killpivot );
        saram.fighting[job].RemoveAt( killpivot );
        saram.love[job].RemoveAt( killpivot );
        saram.life[job].RemoveAt( killpivot );
        saram.num[job]--;
    }
    public void citizenRearrange()
    {
        if(citizenPivot[tabNum] == 0) upObj.SetActive(false);
        else upObj.SetActive(true);
        while(saram.num[tabNum] - citizenPivot[tabNum] < 0) citizenPivot[tabNum]-=3;
        switch(saram.num[tabNum] - citizenPivot[tabNum])
        {
            case 0:
                this.transform.GetChild(6).gameObject.SetActive(false); // group1
                goto case 1;
            case 1:
                this.transform.GetChild(7).gameObject.SetActive(false); // group2
                goto case 2;
            case 2:
                this.transform.GetChild(8).gameObject.SetActive(false); // group3
                goto case 3;
            case 3:
                downObj.SetActive(false);
                break;
            default:
                downObj.SetActive(true);
                break;
        }

        for(int i = 0; i < Mathf.Min(saram.num[tabNum] - citizenPivot[tabNum],3); i++)
        {
            
            int temp_i = citizenPivot[tabNum]+i;
            Debug.Log($"temp_i is {temp_i}");
            GameObject tempGroup = this.transform.GetChild(6+i).gameObject;
            TextMeshPro tempGroupname = tempGroup.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
            tempGroup.SetActive(true);
            tempGroupname.text = saram.nickname[tabNum][temp_i];

        }
        
    }
}
