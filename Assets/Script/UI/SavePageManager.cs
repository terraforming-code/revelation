using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavePageManager : MonoBehaviour
{
    private string profileName; /* 저장할 프로필 이름 */
    private TextMeshPro saveStateText; /* Save 페이지에 표시되는 텍스트 */
    private string saveResult;

    void Awake()
    {
        saveStateText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshPro>();
    }
    void Start()
    {
        Debug.Log("SavePageManager: Start");        
        saveResult = SaveManager.SaveGame(profileName); /* Save Game */      
        switch(saveResult) {
            case "success":
                saveStateText.text = "Successfully Saved the Game.";   
                break;
            case "profileAlreadyExists":
                saveStateText.text = "Profile already exists. Please use another name.";   
                break;
            case "fail":
                saveStateText.text = "Cannot Save the Game. Please Try Again.";   
                break;
        }
        MenuManager.Instance.Invoke("OpenMenuPage", 3); /* 3초 기다린 후 Save 페이지 닫고 Menu 페이지로 돌아감. */
    }

    void Update()
    {
        
    }
}
