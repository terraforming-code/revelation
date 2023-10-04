using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using UnityEngine;
// using UnityEngine.Device;
using Newtonsoft.Json;

public static class SaveManager
{
    private static readonly string saveFolder = Application.persistentDataPath +"/GameData";
    public static string SaveFolder => saveFolder;

    /* profileName에 해당하는 게임 데이터 json 파일을 찾아 SaveProfile 오브젝트로 로드 */
    public static SaveProfile<T> Load<T>(string profileName) where T: SaveProfileData
    {
        if (!File.Exists($"{saveFolder}/{profileName}")) /* Exception: Profile Not Found */
            throw new Exception($"Save Profile {profileName} not found.");

        var fileContents = File.ReadAllText($"{saveFolder}/{profileName}");
        Debug.Log($"Successfully loaded {saveFolder}/{profileName}");

        return JsonConvert.DeserializeObject<SaveProfile<T>>(fileContents); /* Decrypt */
    }

    /* 게임 데이터 오브젝트를 json 파일로 변환 및 저장 */
    public static void Save<T>(SaveProfile<T> saveProfile) where T: SaveProfileData
    {
        if (File.Exists($"{saveFolder}/{saveProfile.name}")) /* Exception: Profile Already Exists */
            throw new Exception($"Save Profile {saveProfile.name} already exists.");

        var jsonString = JsonConvert.SerializeObject(saveProfile, Formatting.Indented, new JsonSerializerSettings{
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        if (!Directory.Exists(saveFolder)) /* Directory가 존재하지 않는 경우 파일을 쓰기 전에 디렉토리를 새로 생성함 */
            Directory.CreateDirectory(saveFolder);
        File.WriteAllText($"{saveFolder}/{saveProfile.name}", jsonString); /* Encrypt */
    }

    /* 게임 저장. 게임 중 저장 UI에 의해 호출됨. */
    public static string SaveGame(string name)
    {
        var saveData = new GameSaveData{};
        var saveProfile = new SaveProfile<GameSaveData>(name, saveData);
        Save(saveProfile);
        return "success";
    }

    /* 저장된 게임 로드. 게임 중 로드 UI에 의해 호출됨. */
    public static void LoadGame(string profileName)
    {   
        SaveProfile<GameSaveData> saveProfile = Load<GameSaveData>(profileName); 
    }

    public static IEnumerable<string> GetFiles()
    {
        return from file in Directory.EnumerateFiles(SaveManager.SaveFolder) select file;
    }

}

