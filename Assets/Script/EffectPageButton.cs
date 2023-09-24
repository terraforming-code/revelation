using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPageButton : MonoBehaviour
{
    SpriteRenderer pageButtonRender;
    public Sprite nextButtSprite, prevButtSprite;
    EffectManager effectManager;
    // Start is called before the first frame update
    void Start()
    {
        pageButtonRender = this.GetComponent<SpriteRenderer>();
        effectManager = this.transform.parent.gameObject.GetComponent<EffectManager>();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        this.transform.position = new Vector3(-this.transform.position.x,this.transform.position.y,0);
        if(effectManager.currentPage == 0) pageButtonRender.sprite = prevButtSprite;
        else pageButtonRender.sprite = nextButtSprite;
        effectManager.effectPageChange();
    }
}
