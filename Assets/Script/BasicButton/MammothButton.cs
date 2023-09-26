using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MammothButton : MonoBehaviour
{
    SpriteRenderer thisRender;
    public GameObject MammothInfoWindow;
    public Sprite openSprite, closeSprite;
    int mammothWindowopening = -1; // opening = 1, closing = -1;
    bool mammothWindowmoving = false;
    void Start()
    {
        thisRender = this.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(mammothWindowmoving) {
            MammothInfoWindow.transform.position += new Vector3(mammothWindowopening * Time.deltaTime * 6,0,0);
            if((MammothInfoWindow.transform.position.x <= 5f && mammothWindowopening == -1) || (MammothInfoWindow.transform.position.x >= 8.6f && mammothWindowopening == 1))
                mammothWindowmoving = false;
        }
    }
    void OnMouseDown()
    {
        if(mammothWindowopening == 1) {
            thisRender.sprite = openSprite;
            mammothWindowmoving = true;
            mammothWindowopening = -1;
        }
        else {
            thisRender.sprite = closeSprite;
            mammothWindowmoving = true;
            mammothWindowopening = 1;
        }
    }
}
