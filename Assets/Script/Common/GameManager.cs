using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; /* For Scene move */

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static bool isPaused; /* Pause 여부 */
    public static bool IsPaused => isPaused;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadScene("MainScene");        
    }
    // Update is called once per frame
    void Update()
    {
        
    }    
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    /* Pause & Resume */
    public static void Pause()
    {
        isPaused = true;
        AudioManager.PauseBGM();
        Time.timeScale = 0f;
    }
    public static void Resume()
    {
        Time.timeScale = 1f;
        AudioManager.UnPauseBGM();
        isPaused = false;
    }
}
