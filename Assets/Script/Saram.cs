using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saram : MonoBehaviour
{

    public string[] nicknameTag1 = new string[]{"Lone","North","Red","Greedy","Dance with","Brave","Shadow","South","Wet","Hot","Winter","Silver"};
    public string[] nicknameTag2 = new string[]{"Star","Pig","Spear","Sword","Wind","Hawk","Wolf","Sheep","River","Mountain","Bear","Muscle"}; 
    public int[] num = new int[]{0,0,0};

    public List<List<string>> nickname = new List<List<string>>();
    public List<List<float>> holy = new List<List<float>>();
    public List<List<float>> farming = new List<List<float>>();
    public List<List<float>> eating = new List<List<float>>();
    public List<List<float>> fighting = new List<List<float>>();
    public List<List<float>> love = new List<List<float>>();
    public List<List<float>> life = new List<List<float>>();
    


    
    void Awake()
    {
        for(int i = 0; i<3; i++) {
            nickname.Add(new List<string>());
            holy.Add(new List<float>());
            farming.Add(new List<float>());
            eating.Add(new List<float>());
            fighting.Add(new List<float>());
            love.Add(new List<float>());
            life.Add(new List<float>());
        }


        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
