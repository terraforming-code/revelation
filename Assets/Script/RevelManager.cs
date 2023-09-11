using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevelManager : MonoBehaviour
{
    public GameObject invenWindow;
    InvenManager invenBox;
    int pivot;
    public bool reveling = false;
    public int revelNum;
    public GameObject revelObj;
    // Start is called before the first frame update
    void Awake()
    {
        invenBox = invenWindow.GetComponent<InvenManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if(hit.collider != null)
            {
                GameObject click_obj = hit.transform.gameObject;
                if(click_obj.name == "RevelConfirm") {
                    invenBox.invenNumBox.RemoveAt(pivot);
                    Destroy(invenBox.invenObjBox[pivot]);
                    invenBox.invenObjBox.RemoveAt(pivot);
                    invenBox.invenRearrange();
                    reveling = false;
                    this.gameObject.SetActive(false);
                }
                if(click_obj.name == "RevelCancel") {
                    reveling = false;
                    this.gameObject.SetActive(false);                   
                }
            }
        }
        
    }
    public void RevelOpen(int num)
    {
        
        pivot = num;

        reveling = true;
        revelNum = invenBox.invenNumBox[num];
        
        revelObj.GetComponent<SpriteRenderer>().sprite = invenBox.invenObjBox[num].GetComponent<SpriteRenderer>().sprite;
        //revelObj = Instantiate(invenBox.invenObjBox[num]);
        
    }
}
