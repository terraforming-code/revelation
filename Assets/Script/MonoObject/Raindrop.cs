using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raindrop : MonoBehaviour
{
    public float dropSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = new Vector3(Random.Range(-24f,24f),Random.Range(0f,60f * dropSpeed)+10f,0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= new Vector3(0,30f * Time.deltaTime * dropSpeed,0);
        if(this.transform.position.y <= -2f) Destroy(this.gameObject);
    }
}
