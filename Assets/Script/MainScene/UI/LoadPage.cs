using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadPage : MonoBehaviour
{
    public GameObject profileButton;
    private List<GameObject> profileButtonList = new List<GameObject>();
    void Awake()
    {
        // gameObject.SetActive(false);
    }
    void Start()
    {
        gameObject.SetActive(true);
        PoolProfiles();
    }
    void Update()
    {
        
    }
    public void Open()
    {
        // gameObject.SetActive(true);
        // PoolProfiles();
    }
    private void PoolProfiles()
    {
        GameObject tmp;
        var files = SaveManager.GetFiles();
        Debug.Log("LoadPage: List of Files");
        foreach (var file in files)
        {
            Debug.Log("{0}" + Path.GetFileName(file));
            tmp = Instantiate(profileButton);
            tmp.transform.SetParent(transform.GetChild(1)); /* LoadPage - Body - {ProfileButton}의 게층으로 설정. */
            tmp.GetComponent<ProfileButton>().Set(Path.GetFileName(file));
            profileButtonList.Add(tmp);
        }
    } 
}
