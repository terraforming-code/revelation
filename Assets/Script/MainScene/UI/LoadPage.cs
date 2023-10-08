using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadPage : MonoBehaviour
{
    public GameObject profileGroup;
    private List<GameObject> profileGroupList = new List<GameObject>();
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
    public void PoolProfiles()
    {
        /* Remove All ProfileGroups */
        foreach (Transform child in transform.GetChild(1)) {
            GameObject.Destroy(child.gameObject);
        }
        GameObject tmp;
        var files = SaveManager.GetFiles();

        /* Pool ProfileGroups */
        foreach (var file in files)
        {
            Debug.Log("{0}" + Path.GetFileName(file));
            tmp = Instantiate(profileGroup);
            tmp.transform.SetParent(transform.GetChild(1)); /* LoadPage - Body - {ProfileButton}의 계층으로 설정. */
            tmp.GetComponent<ProfileGroup>().Set(Path.GetFileName(file));
            profileGroupList.Add(tmp);
        }
    } 
}
