using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButton : MonoBehaviour
{
    SpriteRenderer thisRender;
    public GameObject EnemyInfoWindow;
    public Sprite openSprite, closeSprite;
    int enemyWindowopening = -1; // opening = 1, closing = -1;
    bool enemyWindowmoving = false;
    void Start()
    {
        thisRender = this.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(enemyWindowmoving) {
            EnemyInfoWindow.transform.position += new Vector3(enemyWindowopening * Time.deltaTime * 6,0,0);
            if((EnemyInfoWindow.transform.position.x <= 4.3f && enemyWindowopening == -1) || (EnemyInfoWindow.transform.position.x >= 8.8f && enemyWindowopening == 1))
                enemyWindowmoving = false;
        }
    }
    void OnMouseDown()
    {
        if(enemyWindowopening == 1) {
            thisRender.sprite = openSprite;
            enemyWindowmoving = true;
            enemyWindowopening = -1;
        }
        else {
            thisRender.sprite = closeSprite;
            enemyWindowmoving = true;
            enemyWindowopening = 1;
        }
    }
}
