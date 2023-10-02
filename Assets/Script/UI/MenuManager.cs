using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UI = UnityEngine.UI;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class MenuManager : MonoBehaviour
{
    private static bool isPaused; /* 게임 일시정지 여부 */
    private static GameObject menuWindow; /* 메뉴 창 */

    /* Pages */
    private static GameObject currentPage; /* 현재 활성화된 페이지: 항상 1개만 활성화 */
    private GameObject menuPage; /* 기본 Menu 페이지 */
    // private GameObject confirmQuitPage; /* Quit Conifrm 창 */

    /* Buttons */
    private Button ResumeButton, SettingButton, SaveButton, QuitButton;

    void Awake()
    {
        menuWindow = transform.Find("MenuWindow").gameObject;
        menuPage = menuWindow.transform.Find("MenuPage").gameObject;

        var buttonToHandler = new Dictionary<string, UnityAction>(){
            {"SettingButton", handleClickSettingButton},
            {"SaveButton", handleClickSaveButton},
            {"QuitButton", handleClickQuitButton},
        };

        foreach (Transform child in menuPage.transform.Find("Body").transform) /* MenuPage 의 각 Button을 Handler와 연결. */
        {
            child.Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(buttonToHandler[child.name]);
        }
        

        foreach (Transform child in menuWindow.transform) /* MenuWindow 의 페이지들 모두 비활성화하고 시작. */
        {
            Transform head = child.Find("Head");
            head.Find("CloseButton").Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(CloseMenuWindow);
            head.Find("BackButton")?.Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(()=>OpenPage("MenuPage"));
            child.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MenuManager: Start");
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    /* MenuWindow */
    public void handleClickMenuButton()
    {
        if(isPaused) /* Menu 창 닫고 Resume */
        {
            CloseMenuWindow();
        }
        
        else /*  Pause 하고 Menu 창 열기 */
        {
            OpenMenuWindow();
        } 
    }
    public void OpenMenuWindow()
    {
        Debug.Log("MenuManager: OpenMenuWindow");
        Pause();
        menuWindow.transform.position = new Vector3(0,0,0);
        menuWindow.SetActive(true);

        /* Menu 페이지 열기 */
        currentPage = menuPage;
        menuPage.SetActive(true);
    }
    public static void CloseMenuWindow()
    {
        /* currentPage 초기화 */
        currentPage.SetActive(false);
        currentPage = null;

        menuWindow.SetActive(false);
        Resume();
    }
    /* Pages */
    // public void OpenPage(GameObject page)
    // {
    //     if(currentPage != page)
    //     {
    //         currentPage.SetActive(false);
    //         page.SetActive(true);
    //         currentPage = page;
    //     }
    // }
    public static void OpenPage(string pageName)
    {
        GameObject page = menuWindow.transform.Find(pageName).gameObject;
        if(currentPage != page)
        {
            currentPage.SetActive(false);
            page.SetActive(true);
            currentPage = page;
        }
    }

    /* Menu Button Handlers */
    public static void handleClickSettingButton()
    {
        OpenPage("SettingPage");
    }
    public static void handleClickSaveButton()
    {
        OpenPage("SavePage");
    }
    public static void handleClickQuitButton()
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
    //         CloseMenuWindow();
    //     }
    //     else
    //     {
    //         OpenMenuWindow();
    //     }
    // }
}
