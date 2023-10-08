using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public sealed class SaveProfile
{
    public string name;
    
    public DateTime timestamp;
    public GameSaveData data;

    private SaveProfile()
    {
        
    }
    public SaveProfile(string name)
    {
        this.name = name;
        this.timestamp = DateTime.Now;
        this.data = new GameSaveData{};
    }
}

public record GameSaveData {
    public ResourceSaveData Resource;
    public SeasonSaveData Season;
    public SaramSaveData Saram;
    public TechSaveData Tech;
    public EffectSaveData Effect;
    public InvenSaveData Inven;
    public ShopSaveData Shop;
    public EnemySaveData Enemy;
    public MammothSaveData Mammoth;
    public HellSaveData Hell;
}
public abstract record SaveData {};
public record ResourceSaveData: SaveData
{
    public int money,  seasonEat;
    public float food, power, love, grain, defense, farmTech, fightTech, happy, moretax, foodLimit, happyBeforeHell;
    public bool waitNewSeason, poongzak; 

    public ResourceSaveData(
        int money,
        int seasonEat,
        float food, 
        float power, 
        float love, 
        float grain, 
        float defense, 
        float farmTech, 
        float fightTech, 
        float happy,
        float moretax,
        float foodLimit,
        float happyBeforeHell,
        bool waitNewSeason,    
        bool poongzak
    ){
        this.money = money;
        this.food = food; 
        this.power = power; 
        this.love = love; 
        this.grain = grain; 
        this.defense = defense; 
        this.farmTech = farmTech; 
        this.fightTech = fightTech;
        this.happy = happy;
        this.moretax = moretax;
        this.seasonEat = seasonEat;
        this.foodLimit = foodLimit;
        this.waitNewSeason = waitNewSeason;    
        this.poongzak = poongzak;
        this.happyBeforeHell = happyBeforeHell;
    }
} 
public record SeasonSaveData: SaveData
{
    public float gamespeed;
    public float season;
    public float seasonstop;
    public bool nightTrigger;    
    public bool hellEventEndTrigger;    
    public Vector3 pivotPosition;
    public SeasonSaveData(float gamespeed, float season, float seasonstop, bool nightTrigger, bool hellEventEndTrigger, Vector3 pivotPosition)
    {
        this.gamespeed = gamespeed;
        this.season = season;
        this.seasonstop = seasonstop;
        this.nightTrigger = nightTrigger;
        this.hellEventEndTrigger = hellEventEndTrigger;
        this.pivotPosition = pivotPosition;
    }
}
public record SaramSaveData: SaveData
{
    public int[] num;
    public List<List<int>> code;
    public List<List<string>> nickname;
    public List<List<float>> holy;
    public List<List<float>> farming;
    public List<List<float>> eating;
    public List<List<float>> fighting;
    public List<List<float>> love;
    public List<List<float>> life;

    public List<List<int>> char1; 
    public List<List<int>> char2;
    public List<List<int>> char3;
    public List<List<int>> head; 
    public List<List<bool>> healed;

    public SaramSaveData(int[] num, List<List<int>> code, List<List<string>> nickname, List<List<float>> holy, List<List<float>> farming, List<List<float>> eating, List<List<float>> fighting, List<List<float>> love, List<List<float>> life, List<List<int>> char1, List<List<int>> char2, List<List<int>> char3, List<List<int>> head, List<List<bool>> healed)
    {
        this.num = num;
        this.code = code;
        this.nickname = nickname;
        this.holy = holy;
        this.farming = farming;
        this.eating = eating;
        this.fighting = fighting;
        this.love = love;
        this.life = life;
        this.char1 = char1; 
        this.char2 = char2;
        this.char3 = char3;
        this.head = head;
        this.healed = healed;
    }
}
public record ShopSaveData: SaveData
{
    public int shopRerollPrice;
    public bool isHellRevealed;
    public int[] shops;
    public bool waitspring;
    public ShopSaveData(int shopRerollPrice, bool isHellRevealed, int[] shops, bool waitspring)
    {
        this.shopRerollPrice = shopRerollPrice;
        this.isHellRevealed = isHellRevealed;
        this.shops = shops;
        this.waitspring = waitspring;
    }
}
public record InvenSaveData: SaveData
{
    public List<int> invenNumBox;
    public int invenSelect;
    public InvenSaveData(List<int> invenNumBox, int invenSelect)
    {
        this.invenNumBox = invenNumBox;
        this.invenSelect = invenSelect;
    }
}
public record TechSaveData: SaveData
{
    public int[] enable;    
    public int ironAgeComing;
    public bool ironAgeStart;
    public bool ironAge;
    public TechSaveData(int[] enable, int ironAgeComing, bool ironAgeStart, bool ironAge)
    {
        this.enable = enable;
        this.ironAgeComing = ironAgeComing; 
        this.ironAgeStart = ironAgeStart; 
        this.ironAge = ironAge; 
    }
}
public record EffectSaveData: SaveData
{
    public int[] enable;    
    public EffectSaveData(int[] enable)
    {
        this.enable = enable;
    }
}
public record EnemySaveData: SaveData
{
    public int enemyLife;
    public int fightDate;    
    public int fightCounter;
    public float newenemyPower; 
    public float enemyPower;
    public EnemySaveData(int enemyLife, int fightDate, int fightCounter, float newenemyPower, float enemyPower){
        this.enemyLife = enemyLife;
        this.fightDate = fightDate;    
        this.fightCounter = fightCounter;
        this.newenemyPower = newenemyPower; 
        this.enemyPower = enemyPower;   
    }
}
public record MammothSaveData: SaveData
{
    public int HuntDate;
    public int huntedExperience;
    public float mammothPower;
    public float mammothFood;
    public MammothSaveData(int HuntDate, int huntedExperience, float mammothPower, float mammothFood)
    {
        this.HuntDate = HuntDate;
        this.huntedExperience = huntedExperience;
        this.mammothPower = mammothPower;
        this.mammothFood = mammothFood;
    }
}
public record HellSaveData: SaveData
{    
    public int upcomingHell;
    public int hellPrice;
    public float hellDie;
    public float hellDieNum;    
    public float hellDestroy;
    public float hellEventCounter;
    public bool guardHell;
    public bool eyeOn; 
    public bool bigLose;
    public bool hellLazy;
    public bool experienceFlood;
    public bool eventTriggerOn;

    public HellSaveData(int upcomingHell, int hellPrice, float hellDie, float hellDieNum, float hellDestroy, float hellEventCounter, bool guardHell, bool eyeOn, bool bigLose, bool hellLazy, bool experienceFlood, bool eventTriggerOn)
    {
        this.upcomingHell = upcomingHell;
        this.hellPrice = hellPrice;
        this.hellDie = hellDie; 
        this.hellDieNum = hellDieNum; 
        this.hellDestroy = hellDestroy;
        this.hellEventCounter = hellEventCounter;
        this.guardHell = guardHell;
        this.eyeOn = eyeOn; 
        this.bigLose = bigLose;
        this.hellLazy = hellLazy;
        this.experienceFlood = experienceFlood;
        this.eventTriggerOn = eventTriggerOn;
    }
}