using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutClickEvent : MonoBehaviour
{
    public Sprite HutInnerSprite, HutOuterSprite;
    public bool Inside = false;
    // Start is called before the first frame update
    void OnMouseDown() {
        if(!Inside) {
        
            this.GetComponent<SpriteRenderer>().sprite = HutInnerSprite;
            this.GetComponent<SpriteRenderer>().sortingOrder = 1;
            this.transform.localPosition = new Vector3(0,0,0.1f);
            Inside = !Inside;
        
        }
        else {
            this.GetComponent<SpriteRenderer>().sprite = HutOuterSprite;
            this.GetComponent<SpriteRenderer>().sortingOrder = 3;
            this.transform.localPosition = new Vector3(0,0,-0.1f);
            Inside = !Inside;
        }
    }
}
