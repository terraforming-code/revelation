using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonManager : MonoBehaviour
{
    private GameObject menuWindow;
    void Awake()
    {
        /* Button - Handler Method 연결 */
        transform.Find("HelpButton").Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(HandleClickHelpButton);
        transform.Find("MenuButton").Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(HandleClickMenuButton);
    }    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void HandleClickMenuButton()
    {
        WindowManager.Instance.Open("Menu");
    }
    void HandleClickHelpButton()
    {
        
    }
}
