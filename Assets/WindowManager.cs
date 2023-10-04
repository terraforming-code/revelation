using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    void Awake()
    {
        foreach (Transform child in transform) /* MenuWindow 의 페이지들 모두 비활성화하고 시작. */
        {
            child.Find("Head").Find("CloseButton").Find("Button")?.gameObject.GetComponent<Button>().onClick.AddListener(Close);
            head.Find("BackButton")?.Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(()=>OpenPage("MenuPage"));
            child.gameObject.SetActive(false);
        }            
    }

    public void Close(){}
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
