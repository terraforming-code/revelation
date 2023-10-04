using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public sealed class SaveProfile<T> where T: SaveProfileData
{
    public string name;
    public T saveData;

    private SaveProfile()
    {
        
    }

    public SaveProfile(string name, T saveData)
    {
        this.name = name;
        this.saveData = saveData;
    }
}

public abstract record SaveProfileData {};


public record GameSaveData: SaveProfileData {
    private ResourceSaveData resourceSaveData;
}
public record ResourceSaveData
{
    private int money;
    private float food, power, love, grain, defense, farmTech, fightTech;
    private float happy;
    private float moretax;
    private int seasonEat;
    private float foodLimit;
    private bool waitNewSeason; 
    private bool poongzak;
    private float happyBeforeHell;  
}