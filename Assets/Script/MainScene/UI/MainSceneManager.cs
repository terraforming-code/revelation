using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    private Button NewGameButton;
    void Awake()
    {
        NewGameButton = transform.Find("NewGameButton").Find("Button").GetComponent<Button>();

        NewGameButton.onClick.AddListener(onClickNewGameButton);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void onClickNewGameButton()
    {   
        GameManager.LoadScene("SampleScene");
    }
}
