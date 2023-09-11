using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenManager : MonoBehaviour
{
    public GameObject CardBox;
    public GameObject RevelWindow; 
    RevelManager RevelBox;

    public int count = 0;
    public List<int> invenNumBox = new List<int>();
    public List<GameObject> invenObjBox = new List<GameObject>();
    
    int invenSelect = -1;
    GameObject invenSelectObj;

    
    // Start is called before the first frame update
    void Start()
    {
        invenSelectObj = this.transform.GetChild(7).gameObject;
        RevelBox = RevelWindow.GetComponent<RevelManager>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if(hit.collider != null)
            {
                GameObject click_obj = hit.transform.gameObject;
                if(click_obj.name == "RevelButton" && invenSelect != -1 && !RevelBox.reveling) {
                    Debug.Log($"RevelOpen : {invenSelect} {invenNumBox[invenSelect]}");
                    RevelWindow.SetActive(true);
                    RevelBox.reveling = true;
                    RevelBox.RevelOpen(invenSelect);
                    invenSelect = -1;
                    invenSelectObj.SetActive(false);
                }
                else if(!RevelBox.reveling) {
                    switch(click_obj.name) {
                        case "InvenCard (1)" :
                            if(invenNumBox.Count >= 1) {
                                invenSelect = 0;
                                invenSelectObj.SetActive(true);
                                invenSelectObj.transform.position = this.transform.GetChild(0).position;
                            }
                            break;
                        case "InvenCard (2)" :
                            if(invenNumBox.Count >= 2) {
                                invenSelect = 1;
                                invenSelectObj.SetActive(true);
                                invenSelectObj.transform.position = this.transform.GetChild(1).position;
                            }
                            break;
                        case "InvenCard (3)" :
                            if(invenNumBox.Count >= 3) {
                                invenSelect = 2;
                                invenSelectObj.SetActive(true);
                                invenSelectObj.transform.position = this.transform.GetChild(2).position;
                            }
                            break;
                        case "InvenCard (4)" :
                            if(invenNumBox.Count >= 4) {
                                invenSelect = 3;
                                invenSelectObj.SetActive(true);
                                invenSelectObj.transform.position = this.transform.GetChild(3).position;
                            }
                            break;
                        case "InvenCard (5)" :
                            if(invenNumBox.Count >= 5) {
                                invenSelect = 4;
                                invenSelectObj.SetActive(true);
                                invenSelectObj.transform.position = this.transform.GetChild(4).position;
                            }
                            break;
                        case "InvenCard (6)" :
                            if(invenNumBox.Count >= 6) {
                                invenSelect = 5;
                                invenSelectObj.SetActive(true);
                                invenSelectObj.transform.position = this.transform.GetChild(5).position;
                            }
                            break;
                    }
                }
            }
        }
    }
    public void invenAdd(int num)
    {
        invenNumBox.Add(num);
        GameObject tempObj = Instantiate(CardBox.transform.GetChild(num).gameObject);
        tempObj.transform.SetParent(this.transform);
        invenObjBox.Add(tempObj);
        invenRearrange();
    }
    public void invenRearrange()
    {
        for(int i = 0; i < invenObjBox.Count; i++)
        {
            invenObjBox[i].transform.position = this.transform.GetChild(i).position;
        }
    }

}
