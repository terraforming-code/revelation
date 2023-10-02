using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PriestCharChangeManager : MonoBehaviour
{
    public GameObject Resource;
    Saram saram;
    bool opened = false;
    TextMeshPro charChangeText1, charChangeText2, charChangeText3;
    // Start is called before the first frame update
    void Awake()
    {
        saram = Resource.GetComponent<Saram>();
        charChangeText1 = this.transform.GetChild(0).GetComponent<TextMeshPro>();
        charChangeText2 = this.transform.GetChild(1).GetComponent<TextMeshPro>();
        charChangeText3 = this.transform.GetChild(2).GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(opened)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

                if(hit.collider != null)
                {
                    GameObject click_obj = hit.transform.gameObject;
                    switch(click_obj.name) {
                        case "PriestCharChange1" :
                            if(saram.num[0]==1) saram.char1[0][0] = Random.Range(0,saram.charTag1.Length);
                            opened = false;
                            this.gameObject.SetActive(false);
                            break;
                        case "PriestCharChange2" :
                            if(saram.num[0]==1) saram.char2[0][0] = Random.Range(0,saram.charTag2.Length);
                            opened = false;
                            this.gameObject.SetActive(false);
                            break;
                        case "PriestCharChange3" :
                            if(saram.num[0]==1) saram.char3[0][0] = Random.Range(0,saram.charTag3.Length);
                            opened = false;
                            this.gameObject.SetActive(false);
                            break;
                    }
                }

            
            }
        }
    }
    public void priestCharChangeOpen()
    {
        saram = Resource.GetComponent<Saram>();
        opened = true;
        Debug.Log($"priestCharChangeOpen {saram.char1[0][0]} {saram.char2[0][0]} {saram.char3[0][0]}");

        if(saram.char1[0][0]==-10) charChangeText1.text = "God's knight";
        else if(saram.char1[0][0]==-11) charChangeText1.text = "God's farmer";
        else charChangeText1.text = saram.charTag1[saram.char1[0][0]];
        if(saram.char2[0][0]==-8) charChangeText2.text = "Prophet";
        else charChangeText2.text = saram.charTag2[saram.char2[0][0]];
        if(saram.char3[0][0]==-9) charChangeText3.text = "Technician";
        else charChangeText3.text = saram.charTag3[saram.char3[0][0]];
    }
}
