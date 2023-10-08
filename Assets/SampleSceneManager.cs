using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSceneManager : Singleton<SampleSceneManager>
{
    private static bool isPaused; /* Pause 여부 */
    public static bool IsPaused => isPaused;
    void Start()
    {   
        /* 게임 일시정지 후 로드된 데이터 적용 */
        GameManager.Pause();
        SaveManager.Instance.FindAllSavableObjects();
        SaveManager.Instance.InitializeGame();
        
        /* 데이터 적용이 끝났으므로 Resume */
        GameManager.Resume();
    }
    void Update()
    {
        
    }

}
