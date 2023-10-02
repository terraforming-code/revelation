using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource mainBGM;
    // Start is called before the first frame update
    void Awake()
    {
        AudioManager.SetBGM(mainBGM);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
