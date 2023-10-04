using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenMotionManager : MonoBehaviour
{
    public GameObject Resource;
    Saram saram;
    public GameObject CavemanObj;
    public GameObject CitizenBriefTab;
    public List<GameObject> CavemanObjList = new List<GameObject>();
    public List<int> CavemanCodeList = new List<int>();
    public int[] num = new int[]{0,0,0};

    // Start is called before the first frame update
    void Start()
    {
        saram = Resource.GetComponent<Saram>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 3; i ++) {
            if(num[i]*3-saram.num[i] < 0)
            {
                
                if(Random.Range(0,100) == 17) {num[i]++; CavemanAdd(i);}
            }
        }
    }
    public void CavemanAdd(int job)
    {
        bool zungbok;
        for(int i = 0; i < saram.num[job]; i++)
        {
            zungbok = false;
            for(int j = 0; j < CavemanCodeList.Count; j++) {
                if(saram.code[job][i] == CavemanCodeList[j]) {
                    zungbok = true;
                    break;
                }
            }
            if(!zungbok) {
                GameObject CavemanObjTemp = Instantiate(CavemanObj, this.transform);
                int dirTemp = Random.Range(0,2)*2-1; // -1 : <- , 1 : ->
                CavemanObjTemp.GetComponent<Caveman>().citizenBriefTab = CitizenBriefTab;
                CavemanObjTemp.GetComponent<Caveman>().code = saram.code[job][i];
                CavemanObjTemp.GetComponent<Caveman>().nickname = saram.nickname[job][i];
                CavemanObjTemp.GetComponent<Caveman>().dir = dirTemp;
                CavemanObjTemp.GetComponent<Caveman>().job = job;
                CavemanObjTemp.transform.localPosition = new Vector3(dirTemp*-11,0,0);
                CavemanObjTemp.transform.localScale = new Vector3(dirTemp*0.7f,0.7f,1);
                CavemanObjList.Add(CavemanObjTemp);

                CavemanCodeList.Add(saram.code[job][i]);
                break;
                
            }
        }
    }
    public void CavemanJobMove(int code, int moveJob)
    {
        for(int k = 0; k < CavemanCodeList.Count; k++) {
            if(CavemanCodeList[k]==code) {
                num[CavemanObjList[k].GetComponent<Caveman>().job]--;
                num[moveJob]++;
                CavemanObjList[k].GetComponent<Caveman>().job = moveJob;
                // !tool in hand change animation!
                // if movejob is priest, head var change & head change animation
            }
        }
    }
    public void CavemanDestroy(int code)
    {
        for(int k = 0; k < CavemanCodeList.Count; k++) {
            if(CavemanCodeList[k]==code) {
                num[CavemanObjList[k].GetComponent<Caveman>().job]--;
                CavemanObjList[k].GetComponent<Animator>().SetTrigger("dying");
                Destroy(CavemanObjList[k].GetComponent<Caveman>());
                Destroy(CavemanObjList[k],1.2f);
                CavemanObjList.RemoveAt(k);
                CavemanCodeList.RemoveAt(k);
                break;
            }
        }
    }
}
