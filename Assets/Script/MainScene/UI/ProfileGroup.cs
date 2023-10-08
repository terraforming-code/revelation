using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileGroup: MonoBehaviour
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
        transform.Find("Profile").Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = profileName;

        /* 클릭 Handler Method 연결 */
        transform.Find("PlayButton").Find("Button").GetComponent<Button>().onClick.AddListener(HandleClickPlayButton);
        transform.Find("DeleteButton").Find("Button").GetComponent<Button>().onClick.AddListener(HandleClickDeleteButton);
    }

    /* 클릭 시 해당 프로필의 저장된 게임 로드 */
    public void HandleClickPlayButton()
    {
        SaveManager.Load(profileName);
        GameManager.LoadScene("SampleScene");
    }
    public void HandleClickDeleteButton()
    {
        SaveManager.Delete(profileName);
        transform.parent.parent.GetComponent<LoadPage>().PoolProfiles();
    }
}
