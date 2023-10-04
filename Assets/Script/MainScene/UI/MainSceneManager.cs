using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    private GameObject loadWindow;
    void Awake()
    {
        transform.Find("NewGameButton").Find("Button").GetComponent<Button>().onClick.AddListener(HandleClickNewGameButton);
        transform.Find("LoadGameButton").Find("Button").GetComponent<Button>().onClick.AddListener(HandleClickLoadGameButton);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void HandleClickNewGameButton()
    {   
        GameManager.LoadScene("SampleScene");
    }
    void HandleClickLoadGameButton()
    {   
        WindowManager.Instance.Open("Load");
    }
}
