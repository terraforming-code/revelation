using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    /* AudioSources */
    [SerializeField] private AudioSource mainBGM;
    [SerializeField] private AudioSource buttonClickSound;
    
    public static AudioSource ButtonClickSound;
    private static AudioSource MainBGM;

    /* 마스터 볼륨 관련 Variable */
    public static float MasterVolume => masterVolume;
    private static float masterVolume = 1f;

    public override void Awake()
    {
        base.Awake();
        UnmuteAudio();

        /* SerializeField 를 static 변수에 적용 */
        ButtonClickSound = buttonClickSound; 
        MainBGM = mainBGM;
        // MainBGM.Play();
        // ButtonClickSound.Play();
 
    }
    void Start()
    {
    }
    void Update()
    {
        
    }

    /* 마스터 볼륨 관련 Method */
    public static void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        // Debug.Log($"AudioManager-setMasterVolume(): masterVolume = {masterVolume}");
    }
    public static void MuteAudio()
    {
        AudioListener.volume = 0f;    
    }
    public static void UnmuteAudio()
    {
        AudioListener.volume = masterVolume;    
        // Debug.Log($"AudioManager-UnmuteAudio(): masterVolume = {masterVolume}");
    }    

    /* BGM 관련 Method */
    public static void SetBGM(AudioSource bgm, bool play=true)
    {
        // if()
        if (MainBGM != null){
            MainBGM.Stop();
        }
        MainBGM = bgm;
        if(play)
            MainBGM.Play();
    }
    public static void PauseBGM()
    {
        MainBGM.Pause();
    }    
    public static void UnPauseBGM()
    {
        MainBGM.UnPause();
    }  
}
