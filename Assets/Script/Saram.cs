using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Saram : SavableObject
{

    public int codeCounter = 0;
    public string[] nicknameTag1 = new string[]{"Lone","North","Red","Greedy","Dance with","Brave","Shadow","South","Wet","Hot","Winter","Silver"};
    public string[] nicknameTag2 = new string[]{"Star","Pig","Spear","Sword","Wind","Hawk","Wolf","Sheep","River","Mountain","Bear","Muscle"}; 
    public string[] charTag1 = new string[]{"Lazybones","Eager Beaver","Casanova","Celibacy","Aggressive","Dove"};
    public string[] charTag2 = new string[]{"Gourmand","Weak","Inquisitor","Believer","Healthy","Anarchist","Agape"};
    public string[] charTag3 = new string[]{"Challenger","All-thumbs","Jealous","General","Familism","Cold-blooded"};
    
    
    /********** Save Data *********/
    public int[] num = new int[]{0,0,0};
    public List<List<int>> code = new List<List<int>>();
    public List<List<string>> nickname = new List<List<string>>();
    public List<List<float>> holy = new List<List<float>>();
    public List<List<float>> farming = new List<List<float>>();
    public List<List<float>> eating = new List<List<float>>();
    public List<List<float>> fighting = new List<List<float>>();
    public List<List<float>> love = new List<List<float>>();
    public List<List<float>> life = new List<List<float>>();

    public List<List<int>> char1 = new List<List<int>>(); 
    public List<List<int>> char2 = new List<List<int>>();
    public List<List<int>> char3 = new List<List<int>>();
    public List<List<int>> head = new List<List<int>>(); // cody point
    public List<List<bool>> healed = new List<List<bool>>();
    /*******************************/
    public override void Load() {
        SaramSaveData data = SaveManager.Instance.LoadData.Saram;
        num = data.num;
        code = data.code;
        nickname = data.nickname;
        holy = data.holy;
        farming = data.farming;
        eating = data.eating;
        fighting = data.fighting;
        love = data.love;
        life = data.life;
        char1 = data.char1; 
        char2 = data.char2;
        char3 = data.char3;
        head = data.head;
        healed = data.healed;
    }
    public override void Save() {
        SaveManager.Instance.SaveData.Saram = new SaramSaveData(
            num,
            code,
            nickname,
            holy,
            farming,
            eating,
            fighting,
            love,
            life,
            char1, 
            char2,
            char3,
            head,
            healed
        );
    }
    void Awake()
    {
        for(int i = 0; i<3; i++) {
            code.Add(new List<int>());
            nickname.Add(new List<string>());
            holy.Add(new List<float>());
            farming.Add(new List<float>());
            eating.Add(new List<float>());
            fighting.Add(new List<float>());
            love.Add(new List<float>());
            life.Add(new List<float>());
            char1.Add(new List<int>());
            char2.Add(new List<int>());
            char3.Add(new List<int>());
            head.Add(new List<int>());
            healed.Add(new List<bool>());
        }
        
    }
    public void GiveChar(int i, int j, int prop, int propBox) // i,j is index, prop is characteristic number, propBox is characteristic Box number
    {
        if(propBox == 1)
        {
            switch(prop)
            {
                case 0 :
                    farming[i][j] *= 0.5f;
                    fighting[i][j] *= 0.5f;
                    break;
                case 1 :
                    farming[i][j] *= 2;
                    break;
                case 2 :
                    break; // casa nova
                case 3 :
                    love[i][j] = 0f;
                    break;
                case 4 :
                    fighting[i][j] *= 2;
                    break;
                case 5 :
                    fighting[i][j] *= 0.5f;
                    break;
                case -10 : // god's knight
                    fighting[i][j] = 2.5f;
                    break;
                case -11 : // god's farmer
                    farming[i][j] = 2.5f;
                    break;

            }
        }
        else if(propBox == 2)
        {
            switch(prop)
            {
                case 0 :
                    eating[i][j] *= 2;
                    break;
                case 1 :
                    life[i][j] *= 0.5f;
                    break;
                case 2 :
                    holy[i][j] = Mathf.Min(holy[i][j]*1.5f,1);
                    break;
                case 3 :
                    holy[i][j] = Mathf.Min(holy[i][j]*1.2f,1);
                    break;
                case 4 :
                    life[i][j] *= 1.5f;
                    break;
                case 5 :
                    holy[i][j] = Mathf.Max(holy[i][j]*0.5f,0.1f);
                    break;
                case 6 :
                    for(int k = 0;k < char1[i].Count-1;k++)
                        holy[i][k] = Mathf.Min(holy[i][k]*1.1f,1);
                    break; // agape
                case -8 :
                    holy[i][j] = 1f;
                    break;

            }
        }
        /*else if(propBox == 3)
        {
            switch(prop)
            {
                case 0 :
                    //eating[i][j] *= 2;
                    break;
                case 1 :
                    life[i][j] *= 0.5f;
                    break;
                case 2 :
                    holy[i][j] *= 1.5f;
                    break;
                case 3 :
                    holy[i][j] *= 1.2f;
                    break;
                case 4 :
                    life[i][j] *= 1.5f;
                    break;
                case 5 :
                    for(int k = 0;k < char1[i].Count-1;k++)
                        holy[i][k] *= 1.1f;
                    break; // agape

            }
        }*/

    }
    public void HolyAdd(float val)
    {
        for(int i = 1; i < 3; i++)
        {
            for(int j = 0; j < holy[i].Count; j++)
            {
                holy[i][j] = Mathf.Min( Mathf.Max(0.1f,holy[i][j]+val),1f);
            }
        }
    }
    
}
