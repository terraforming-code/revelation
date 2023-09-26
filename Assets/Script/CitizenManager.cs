using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CitizenManager : MonoBehaviour
{
    public GameObject Resource, messageManager;
    public GameObject seasonManager, buildManager;
    BuildingManager buildBox;
    SeasonManager seasonBox;
    Resource resource;
    Saram saram;
    MessageManager messageBox;

    GameObject upObj, downObj, tabPivotObj;
    public int tabNum = 1;
    public int[] citizenPivot = new int[]{0,0,0};

    public int diedCitizenNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        upObj = this.transform.GetChild(3).gameObject;
        downObj = this.transform.GetChild(4).gameObject;
        tabPivotObj = this.transform.GetChild(5).gameObject;
        resource = Resource.GetComponent<Resource>();
        saram = Resource.GetComponent<Saram>();
        seasonBox = seasonManager.GetComponent<SeasonManager>();
        messageBox = messageManager.GetComponent<MessageManager>();
        buildBox = buildManager.GetComponent<BuildingManager>();
        
        // Add proto DB
        for(int i = 0; i<5; i++)
            citizenAdd(true);

        

        

        citizenRearrange();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<3; i++) {
            int j = 0;
            while(j<saram.life[i].Count) {
                saram.life[i][j]-=Time.deltaTime/seasonBox.gamespeed/7*4;
                if(saram.life[i][j]<0f)
                {
                    citizenKill(i,j,2);
                    citizenRearrange();
                }
                else j++;
            }
        }
        for(int i = 0; i < Mathf.Min(saram.num[tabNum] - citizenPivot[tabNum],3); i++)
        {
            int temp_i = citizenPivot[tabNum]+i;
            GameObject tempGroup = this.transform.GetChild(6+i).gameObject;
            GaugeScale(tempGroup.transform.GetChild(3),-3.05f,-1.22f,0.39f,saram.life[tabNum][temp_i]/5); // LIFE
        }
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
                    case "CitizenPromo1" :
                        citizenMove(tabNum,citizenPivot[tabNum] + 0,0); citizenRearrange(); break;
                    case "CitizenPromo2" :
                        citizenMove(tabNum,citizenPivot[tabNum] + 1,0); citizenRearrange(); break;
                    case "CitizenPromo3" :
                        citizenMove(tabNum,citizenPivot[tabNum] + 2,0); citizenRearrange(); break;
                    case "CitizenKill1" :
                        citizenKill(tabNum,citizenPivot[tabNum] + 0,1); citizenRearrange(); break;
                    case "CitizenKill2" :
                        citizenKill(tabNum,citizenPivot[tabNum] + 1,1); citizenRearrange(); break;
                    case "CitizenKill3" :
                        citizenKill(tabNum,citizenPivot[tabNum] + 2,1); citizenRearrange(); break;
                    case "CitizenUp" :
                        citizenPivot[tabNum]-=3; citizenRearrange(); break;
                    case "CitizenDown" :
                        citizenPivot[tabNum]+=3; citizenRearrange(); break;
                }
            }
            
        }
    }
    public void citizenAdd(bool newGame = false)
    {
        int job = Random.Range(0,2)+1;
        string new_nickname = saram.nicknameTag1[Random.Range(0,saram.nicknameTag1.Length)]+" "+saram.nicknameTag2[Random.Range(0,saram.nicknameTag2.Length)];
        saram.nickname[job].Add(new_nickname);
        
        if(newGame) saram.holy[job].Add(Random.Range(0.9f,1.1f)*0.3f); // first citizen
        else {
            int holyTempcounter = 0;
            float holyTempSum = 0f;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < saram.holy[i].Count; j++)
                {
                    if(saram.char2[i][j] != 2 && saram.char2[i][j] != 5)
                    {
                        holyTempcounter++;
                        holyTempSum+=saram.holy[i][j];
                    }
                }
            }
            saram.holy[job].Add(Mathf.Min(Random.Range(0.9f,1.1f)*holyTempSum/holyTempcounter,1f));
            
            
        }
        saram.farming[job].Add((float)Random.Range(8,13) / 10.0f * 10f);
        saram.eating[job].Add((float)Random.Range(8,13) / 10.0f);
        saram.fighting[job].Add((float)Random.Range(8,13) / 10.0f);
        saram.love[job].Add((float)Random.Range(8,13) / 10.0f);
        saram.life[job].Add(Random.Range(1.0f,3.0f));

        saram.char1[job].Add(Random.Range(0,6)); saram.GiveChar(job,saram.char1[job].Count-1,saram.char1[job][saram.char1[job].Count-1],1);
        saram.char2[job].Add(Random.Range(0,7)); saram.GiveChar(job,saram.char2[job].Count-1,saram.char2[job][saram.char2[job].Count-1],2);
        saram.char3[job].Add(Random.Range(0,6)); saram.GiveChar(job,saram.char3[job].Count-1,saram.char3[job][saram.char3[job].Count-1],3);


        saram.num[job]++;

        messageBox.messageAdd("New Live : "+new_nickname);


    }
    public void citizenMove(int job,int pivot,int movejob)
    {
        saram.nickname[movejob].Add(saram.nickname[job][pivot]);
        
        saram.holy[movejob].Add(saram.holy[job][pivot]);
        saram.farming[movejob].Add(saram.farming[job][pivot]);
        saram.eating[movejob].Add(saram.eating[job][pivot]);
        saram.fighting[movejob].Add(saram.fighting[job][pivot]);
        saram.love[movejob].Add(saram.love[job][pivot]);
        saram.life[movejob].Add(saram.life[job][pivot]);

        saram.char1[movejob].Add(saram.char1[job][pivot]);
        saram.char2[movejob].Add(saram.char2[job][pivot]);
        saram.char3[movejob].Add(saram.char3[job][pivot]);


        saram.num[movejob]++;
        citizenKill(job,pivot,-1);
    }
    public void citizenKill(int job,int killpivot,int reason)
    {
        switch(reason)
        {
            case 0 : // hungry
                messageBox.messageAdd(saram.nickname[job][killpivot]+" Starved to Death");
                break;
            case 1 : // god killed
                messageBox.messageAdd(saram.nickname[job][killpivot]+" Received Heavenly Punishment");
                break;
            case 2 : // pass away
                messageBox.messageAdd(saram.nickname[job][killpivot]+" Passed away");
                break;
            case 3 : // Killed in Action
                messageBox.messageAdd(saram.nickname[job][killpivot]+" Killed in Action");
                break;
            case 40 : // fanatic kill
                messageBox.messageAdd(saram.nickname[job][killpivot]+" became victim of the inquisition");
                break;
            case -1 : // didn't die
                break;
        }
        if(job == 0) {
            saram.HolyAdd(-0.15f);
            citizenRearrange();
        }
        if(reason != -1) {
            if(buildBox.build[5] == 1f) resource.happy = Mathf.Max(resource.happy-0.0125f,0.8f);
            else resource.happy = Mathf.Max(resource.happy-0.05f,0.8f);
            diedCitizenNum++;
        }
        saram.nickname[job].RemoveAt( killpivot );
        saram.holy[job].RemoveAt( killpivot );
        saram.farming[job].RemoveAt( killpivot );
        saram.eating[job].RemoveAt( killpivot );
        saram.fighting[job].RemoveAt( killpivot );
        saram.love[job].RemoveAt( killpivot );
        saram.life[job].RemoveAt( killpivot );
        saram.char1[job].RemoveAt( killpivot );
        saram.char2[job].RemoveAt( killpivot );
        saram.char3[job].RemoveAt( killpivot );
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
            GameObject tempGroup = this.transform.GetChild(6+i).gameObject;
            TextMeshPro tempGroupname = tempGroup.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>(); // name
            tempGroup.SetActive(true);
            tempGroupname.text = saram.nickname[tabNum][temp_i];

            GaugeScale(tempGroup.transform.GetChild(2),-3.05f,-1.22f,0.39f,saram.holy[tabNum][temp_i]); // HOLY
            GaugeScale(tempGroup.transform.GetChild(3),-3.05f,-1.22f,0.39f,saram.life[tabNum][temp_i]/5); // LIFE

            if(saram.num[0] == 1) tempGroup.transform.GetChild(4).gameObject.SetActive(false); // promo button
            else tempGroup.transform.GetChild(4).gameObject.SetActive(true);

        }
        
    }
    public void GaugeScale(Transform Obj, float baseLeft, float baseCenter, float baseScale, float num)
    {
        Obj.localPosition = new Vector3( baseLeft + (baseCenter-baseLeft)*num , Obj.localPosition.y,Obj.localPosition.z);
        Obj.localScale = new Vector3(num*baseScale,Obj.localScale.y,1f);
    }
}
