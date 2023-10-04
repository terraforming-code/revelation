using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileButton: MonoBehaviour
{
    private string profileName;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Set(string profileName)
    {   
        this.profileName = profileName;

        /* Profile 표시 */
        transform.Find("Button").Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = profileName;

        /* 클릭 Handler Method 연결 */
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    /* 클릭 시 해당 프로필의 저장된 게임 로드 */
    public void HandleClick()
    {
        SaveManager.LoadGame(profileName);
    }
}
