using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    // static T _instance;
    // public static T Instance => _instance;
    // {
    //     get
    //     {
    //         if(_instance == null) _instance = (T) FindObjectOfType(typeof(T));
    //         if(_instance == null) Debug.LogError("An instance of " + typeof(T) +  " is needed in the scene, but there is none.");
    //         return _instance;
    //     }
    // }
    public virtual string WindowName {get; set;}
    public GameObject CurrentPage; /* 현재 활성화된 페이지: 항상 1개만 활성화 */

    public virtual void Awake(){
        WindowManager.Instance.Add(WindowName, gameObject); /* Window를 Window Manager에 등록 */  
        
        foreach (Transform child in transform) /* Window 의 페이지들 모두 1. 비활성화 2. Close 버튼에 Handler 연결. */
        {
            child.Find("Head")?.Find("CloseButton").Find("Button")?.gameObject.GetComponent<Button>().onClick.AddListener(Close);
            child.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true); /* 배경 활성화 */
    }
    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {        
    }
    public virtual void Open()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(0,0,0);

        /* 첫번째 페이지 열기 */
        CurrentPage = transform.GetChild(1).gameObject;
        CurrentPage.SetActive(true); 
    }
    public virtual void Close()
    {
        /* CurrentPage 초기화 */
        CurrentPage.SetActive(false);
        CurrentPage = null;

        gameObject.SetActive(false);
    }
}