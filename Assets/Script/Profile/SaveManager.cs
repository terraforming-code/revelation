using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using UnityEngine;
// using UnityEngine.Device;
using Newtonsoft.Json;

public class SaveManager : Singleton<SaveManager>
{
    private static string saveFolder;
    public static string SaveFolder => saveFolder;
    private Dictionary<string, SavableObject> nameToSavableObject = new Dictionary<string, SavableObject>();
    private List<SavableObject> savableObjectList;
    private SaveProfile saveProfile;
    public GameSaveData SaveData => Instance.saveProfile.data;
    private SaveProfile loadProfile;
    public GameSaveData LoadData => Instance.loadProfile.data;

    void Start()
    {
        saveFolder = Application.persistentDataPath +"/GameData";
    }

    public void Add(string saveKey, SavableObject savableObject)
    {
        Debug.Log($"SaveManager: saveKey={saveKey}");
        Instance.nameToSavableObject.Add(saveKey, savableObject);
    }

    /* SavableObject 을 구현한 저장할 데이터를 가진 MonoBehaviour 의 리스트 얻기 */
    public void FindAllSavableObjects()
    {
        savableObjectList = new List<SavableObject>(FindObjectsOfType<SavableObject>());
    }

    /* 저장된 파일 리스트 얻기 */
    public static IEnumerable<string> GetFiles()
    {
        return from file in Directory.EnumerateFiles(SaveFolder) select file;
    }

    /* profileName에 해당하는 게임 데이터 json 파일을 찾아 삭제 */
    public static void Delete(string profileName)
    {
        if (!File.Exists($"{saveFolder}/{profileName}")) /* Exception: Profile Not Found */
            throw new Exception($"Save Profile {profileName} not found.");
        File.Delete($"{saveFolder}/{profileName}");
    }

    /* profileName에 해당하는 게임 데이터 json 파일을 찾아 SaveProfile 오브젝트로 로드 */
    public static void Load(string profileName) 
    {
        if (!File.Exists($"{saveFolder}/{profileName}")) /* Exception: Profile Not Found */
            throw new Exception($"Save Profile {profileName} not found.");

        var fileContents = File.ReadAllText($"{saveFolder}/{profileName}");
        Debug.Log($"Successfully loaded {saveFolder}/{profileName}");

        Instance.loadProfile = JsonConvert.DeserializeObject<SaveProfile>(fileContents); /* Decrypt */
    }

    /* 게임 데이터 오브젝트를 json 파일로 변환 및 저장 */
    public static void Save() 
    {
        if (File.Exists($"{saveFolder}/{Instance.saveProfile.name}")) /* Exception: Profile Already Exists */
            throw new Exception($"Save Profile {Instance.saveProfile.name} already exists.");

        var jsonString = JsonConvert.SerializeObject(Instance.saveProfile, Formatting.Indented, new JsonSerializerSettings{
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        if (!Directory.Exists(saveFolder)) /* Directory가 존재하지 않는 경우 파일을 쓰기 전에 디렉토리를 새로 생성함 */
            Directory.CreateDirectory(saveFolder);
        File.WriteAllText($"{saveFolder}/{Instance.saveProfile.name}", jsonString); /* Encrypt */
    }

    /* 게임 저장. 게임 중 저장 UI에 의해 호출됨. */
    public static string SaveGame(string profileName)
    {
        Instance.saveProfile = new SaveProfile(profileName); /* SaveProfile 생성 */
        
        /* 각 요소 저장 */
        foreach (SavableObject savableObject in Instance.savableObjectList)
        {
            savableObject.Save();  
        }
        Save();
        
        Instance.saveProfile = null;
        return "success";
    }

    /* 로드된 데이터를 게임에 적용. 일시정지된 상태에서 SampleSceneManager에 의해 호출됨. */
    public void InitializeGame()
    {   
        if(Instance.loadProfile != null) /* Loaded Game */
        {
            /* 각 요소 로드 */
            foreach (SavableObject savableObject in Instance.savableObjectList)
            {
                Debug.Log($"SaveManager: InitializeGame: (Loaded Game) savableObject={savableObject}");
                savableObject.Load();  
            }     
            /* loadProfile 초기화 */
            Instance.loadProfile = null;
        }
        else /* New Game */
        {
            /* 각 요소 로드 */
            foreach (SavableObject savableObject in Instance.savableObjectList)
            {
                Debug.Log($"SaveManager: InitializeGame: (New Game) savableObject={savableObject}");
                savableObject.CreateNew();  
            }     
            /* loadProfile 초기화 */
            Instance.loadProfile = null;
        }
    }

    /*  Deprecated */
    // /* ProfileButton 클릭 핸들러 */
    // public void HandleClickProfileButton()
    // {
    //     Load(profileName);
    //     GameManager.LoadScene("SampleScene");
    // }

}

