using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caveman : MonoBehaviour
{
    Animator animator;

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
}
