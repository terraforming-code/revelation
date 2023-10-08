using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitPage : MonoBehaviour
{
    void Awake()
    {
        transform.Find("Body").Find("ConfirmButtonSet").Find("ConfirmButton").Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(HandleClickConfirmButton);
        transform.Find("Body").Find("ConfirmButtonSet").Find("CancelButton").Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(HandleClickCancelButton);
    }

    void Update()
    {
        
    }

    void HandleClickConfirmButton()
    {        
        GameManager.LoadScene("MainScene");
    }
    void HandleClickCancelButton(){
        transform.parent.gameObject.GetComponent<MenuWindow>().OpenMenuPage();
    }
}
