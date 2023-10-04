using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UI = UnityEngine.UI;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class MenuWindow : Window
{
    public override string WindowName{get{return "Menu";}}
    private static bool isPaused; /* 게임 일시정지 여부 */

    /* Pages */
    private static GameObject menuPage; /* 기본 Menu 페이지 */
    // private GameObject confirmQuitPage; /* Quit Conifrm 창 */

    /* Buttons */
    private Button ResumeButton, SettingButton, SaveButton, QuitButton;

    public override void Awake()
    {
        base.Awake();
        menuPage = transform.Find("MenuPage").gameObject;

        var buttonToHandler = new Dictionary<string, UnityAction>(){
            {"SettingButton", handleClickSettingButton},
            {"SaveButton", handleClickSaveButton},
            {"QuitButton", handleClickQuitButton},
        };

        foreach (Transform child in menuPage.transform.Find("Body").transform) /* MenuPage 의 각 Button을 Handler와 연결. */
        {
            child.Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(buttonToHandler[child.name]);
        }
        foreach (Transform child in transform) /* 각 페이지의 BackButton 을 클릭 시 메뉴 페이지로 돌아가도록 Handler Method 연결. */
        {
            child.Find("Head")?.Find("BackButton").Find("Button")?.gameObject.GetComponent<Button>().onClick.AddListener(OpenMenuPage);
        }
        isPaused = false;
    }
    public override void Open()
    {
        Debug.Log("MenuManager: Open");
        Pause();
        base.Open();
    }
    /* Pause & Resume */
    public static void Pause()
    {
        isPaused = true;
        AudioManager.PauseBGM();
        Time.timeScale = 0f;
    }
    public static void Resume()
    {
        Time.timeScale = 1f;
        AudioManager.UnPauseBGM();
        isPaused = false;
    }
    /* Menutransform */
    public void handleClickMenuButton()
    {
        if(isPaused) /* Menu 창 닫고 Resume */
        {
            Close();
        }
        
        // else /*  Pause 하고 Menu 창 열기 */
        // {
        //     Open();
        // } 
    }
    // public static void Open()
    // {
    //     Debug.Log("MenuManager: OpenMenutransform");
    //     Pause();
    //     transform.position = new Vector3(0,0,0);
    //     gameObject.SetActive(true);

    //     /* Menu 페이지 열기 */
    //     CurrentPage = menuPage;
    //     menuPage.SetActive(true);
    // }
    // public override void Open()
    // {
    // }
    public override void Close()
    {
        Resume();
    }
    /* Pages */
    public void OpenPage(string pageName)
    {
        GameObject page = transform.Find(pageName).gameObject;
        if(CurrentPage != page)
        {
            CurrentPage.SetActive(false);
            page.SetActive(true);
            CurrentPage = page;
        }
    }

    public void OpenMenuPage() /* 딜레이를 주는 Invoke() Call 을 위해 parameter 가 없는 함수가 필요함. */
    {
        OpenPage("MenuPage");
    }
    public void handleClickSettingButton()
    {
        OpenPage("SettingPage");
    }
    public void handleClickSaveButton()
    {
        OpenPage("SavePage");
    }
    public void handleClickQuitButton()
    {
        OpenPage("ConfirmQuitPage");
    }
    public static void Quit()
    {
        GameManager.LoadScene("MainScene");
    }
    // public void OpenConfirmPage()
    // {
    //     confirmQuitPage.SetActive(true);
    // }
    // public void OpenMenuPage()
    // {
    //     confirmQuitPage.SetActive(true);
    // }
    // public void OnEscape(InputAction.CallbackContext context)
    // {
    //     Debug.Log($"MenuManager=OnEscape(): isPaused = {isPaused}");
    //     if(isPaused)
    //     {
    //         CloseMenutransform();
    //     }
    //     else
    //     {
    //         OpenMenutransform();
    //     }
    // }
}
