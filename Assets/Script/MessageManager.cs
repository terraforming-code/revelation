using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    public GameObject messageUI;
    public List<GameObject> message = new List<GameObject>();
    public List<float> counter = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = message.Count-1; i >= 0; i--)
        {
            counter[i] += Time.deltaTime;
            if(counter[i] > 3f) {
                messageKill(i);
            }
        }
    }
    public void messageAdd(string context)
    {
        GameObject tempMessageUI = Instantiate(messageUI);
        TextMeshPro tempMessageText = tempMessageUI.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        if(message.Count == 4) messageKill(3);
        tempMessageUI.transform.SetParent(this.transform);
        tempMessageUI.transform.position = this.transform.position + new Vector3(0,-1f * message.Count,0);
        tempMessageText.text = context;
        message.Add(tempMessageUI);
        counter.Add(0f);

    }
    public void messageKill(int num)
    {
        Destroy(message[num]);
        message.RemoveAt(num);
        counter.RemoveAt(num);
        for(int i = num; i < message.Count; i++)
            message[i].transform.position = new Vector3(message[i].transform.position.x,message[i].transform.position.y+1f,0);
    }
}
