using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Caveman : MonoBehaviour
{
    public GameObject citizenBriefTab;
    SpriteRenderer citizenBriefTabHead, citizenBriefTabFace;
    TextMeshPro citizenBriefTabName, citizenBriefTabJob;
    Animator animator;
    public string nickname;
    public int code, job, dir;
    bool stopping;
    float stopPos, stopTime, stopCounter=0f;
    float moveSpeed=4f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        stopPos=Random.Range(-6f,6f);
        stopTime=Random.Range(1f,3f);

        citizenBriefTabHead = citizenBriefTab.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        citizenBriefTabFace = citizenBriefTab.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
        citizenBriefTabName = citizenBriefTab.transform.GetChild(1).GetComponent<TextMeshPro>();
        citizenBriefTabJob = citizenBriefTab.transform.GetChild(2).GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if((dir == -1 && stopPos > this.transform.localPosition.x) || (dir == 1 && stopPos < this.transform.localPosition.x)) {
            animator.SetBool("stopping",true);
            stopping = true;
        }

        if(stopping) {
            stopCounter+=Time.deltaTime;
            if(stopTime<stopCounter) {
                animator.SetBool("stopping",false);
                stopping = false;
                stopPos = dir*999f;
            }
        }
        else {
            this.transform.localPosition += new Vector3(dir*moveSpeed*Time.deltaTime,0,0);
        }

        if((dir == -1 && -11f > this.transform.localPosition.x) || (dir == 1 && 11f < this.transform.localPosition.x))
        {
            this.transform.parent.GetComponent<CitizenMotionManager>().CavemanDestroy(code);
        }
    }
    void OnMouseEnter()
    {
        citizenBriefTabHead.sprite = this.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite;
        citizenBriefTabFace.sprite = this.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite;
        citizenBriefTab.SetActive(true);
        citizenBriefTabName.text = nickname;
        switch(job)
        {
            case 0: citizenBriefTabJob.text = "Priest"; break;
            case 1: citizenBriefTabJob.text = "Farmer"; break;
            case 2: citizenBriefTabJob.text = "Warrior"; break;
        }
        
    }
    void OnMouseExit()
    {
        citizenBriefTab.SetActive(false);
    }
}
