using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavePage : MonoBehaviour
{
    private string profileName; /* 저장할 프로필 이름 */
    private TextMeshProUGUI saveStateText; /* Save 페이지에 표시되는 텍스트 */
    private string saveResult;
    

    void Awake()
    {
        // menuManager = GameObject.Find("MenuWindow").gameObject.GetComponent<MenuManager>;
        saveStateText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        Debug.Log("SavePage: Start");        
        profileName = $"testProfile_{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")}";
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
        // menuManager.Invoke("OpenMenuPage", 3); /* 3초 기다린 후 Save 페이지 닫고 Menu 페이지로 돌아감. */
    }

    void Update()
    {
        
    }
}
