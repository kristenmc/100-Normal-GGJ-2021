using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager MiniGameManagerInstance;
    #region Minigame Vars
    [SerializeField] GameObject foodCanvas;
    [SerializeField] GameObject waterCanvas;
    [SerializeField] GameObject gorbageCanvas;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] bool allowForCallbacks;
    #endregion

    #region Beatmap Vars
    [SerializeField] int foodBeatMapLength = 0;
    [SerializeField] int waterBeatMapLength = 0;
    [SerializeField] int gorbageBeatMapLength = 0;
    [SerializeField] List<string> foodBeatMapParts;
    [SerializeField] List<string> waterBeatMapParts;
    [SerializeField] List<string> gorbageBeatMapParts;
    [SerializeField] string generatedBeatMap;
    [SerializeField] int currentBeatmapLocation = 0;
    #endregion

    #region Global Events
    public event Action<int> onBeatCall;
    public void beatCall(int beatInt)
    {
        if(onBeatCall != null)
        {
            onBeatCall(beatInt);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        MiniGameManagerInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            startFoodMinigame();
        }
    }

    #region Start Minigame
    public void startFoodMinigame()
    {
        foodCanvas.SetActive(true);
        GameManager.GameManagerInstance.setMinigameActivity(true);
        pauseMusic();
        startFoodMusic();
        resumeFoodMusic();
        generatedBeatMap = gameObject.GetComponent<BeatMapMaker>().GenerateBeatMap(foodBeatMapLength, foodBeatMapParts);
        allowForCallbacks = true;
        currentBeatmapLocation = 0;
        //temp func for testing callback logic
        MiniGameManagerInstance.onBeatCall += callBackTest;
    }

    public void startWaterMinigame()
    {
        waterCanvas.SetActive(true);
        GameManager.GameManagerInstance.setMinigameActivity(true);
        pauseMusic();
        startWaterMusic();
        generatedBeatMap = gameObject.GetComponent<BeatMapMaker>().GenerateBeatMap(waterBeatMapLength, waterBeatMapParts);
        allowForCallbacks = true;
        currentBeatmapLocation = 0;
        //temp func for testing callback logic
        MiniGameManagerInstance.onBeatCall += callBackTest;
    }

    public void startGorbageMinigame()
    {
        gorbageCanvas.SetActive(true);
        GameManager.GameManagerInstance.setMinigameActivity(true);
        pauseMusic();
        startGorbageMusic();
        generatedBeatMap = gameObject.GetComponent<BeatMapMaker>().GenerateBeatMap(gorbageBeatMapLength, gorbageBeatMapParts);
        allowForCallbacks = true;
        currentBeatmapLocation = 0;
        //temp func for testing callback logic
        MiniGameManagerInstance.onBeatCall += callBackTest;
    }

    public void startShopInteraction()
    {

    }
    #endregion

    public void endMinigame()
    {
        GameManager.GameManagerInstance.setMinigameActivity(false);
        foodCanvas.SetActive(false);
        waterCanvas.SetActive(false);
        gorbageCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        stopFoodMusic();
        stopWaterMusic();
        stopGorbageMusic();
        stopShopMusic();
        resumeWalkMusic();
    }

    void Callback_Function(object in_Cookie, AkCallbackType in_Type, object in_Info)
    {
        if(allowForCallbacks)
        {
            if(currentBeatmapLocation < generatedBeatMap.Length)
            {
                MiniGameManagerInstance.onBeatCall(generatedBeatMap[currentBeatmapLocation]);
            }
            currentBeatmapLocation++;
        }
    }

    public void callBackTest(int i)
    {
        Debug.Log(i);
    }

    public void pauseMusic()
    {
        Debug.Log("Pausing All Music");
        //AkSoundEngine.PostEvent("Pause_Walking_Music", gameObject);
        AkSoundEngine.PostEvent("Pause_Fishing_Music", gameObject);
        AkSoundEngine.PostEvent("Pause_Water_Music", gameObject);
        AkSoundEngine.PostEvent("Pause_Gorbage_Music", gameObject);
        AkSoundEngine.PostEvent("Pause_Shop_Music", gameObject);
    }
    
    #region Resume Music Funcs
    public void resumeWalkMusic()
    {
        Debug.Log("Resuming Walk Music");
        AkSoundEngine.PostEvent("Resume_Walk_Music", gameObject);
    }

    public void resumeFoodMusic()
    {
        Debug.Log("Resuming Food Music");
        AkSoundEngine.PostEvent("Resume_Fishing_Music", gameObject);

    }

    public void resumeWaterMusic()
    {
        Debug.Log("Resuming Water Music");
        AkSoundEngine.PostEvent("Resume_Water_Music", gameObject);

    }

    public void resumeGorbageMusic()
    {
        Debug.Log("Resuming Gorbage Music");
        AkSoundEngine.PostEvent("Resume_Gorbage_Music", gameObject);

    }

    public void resumeShopMusic()
    {
        Debug.Log("Resuming Shop Music");
        AkSoundEngine.PostEvent("Resume_Shop_Music", gameObject);
    }
    #endregion

    #region Stop Music
    public void stopWalkMusic()
    {
        Debug.Log("Stopping Walk Music");
        AkSoundEngine.PostEvent("Stop_Walk_Music", gameObject);
    }

    public void stopFoodMusic()
    {
        Debug.Log("Stopping Food Music");
        AkSoundEngine.PostEvent("Stop_Fishing_Music", gameObject);

    }

    public void stopWaterMusic()
    {
        Debug.Log("Stopping Water Music");
        AkSoundEngine.PostEvent("Stop_Water_Music", gameObject);

    }

    public void stopGorbageMusic()
    {
        Debug.Log("Stopping Gorbage Music");
        AkSoundEngine.PostEvent("Stop_Gorbage_Music", gameObject);

    }

    public void stopShopMusic()
    {
        Debug.Log("Stopping Shop Music");
        AkSoundEngine.PostEvent("Stop_Shop_Music", gameObject);
    }
    #endregion
    
    #region Start Music
    public void startWalkMusic()
    {
        Debug.Log("Starting Walk Music");
        AkSoundEngine.PostEvent("Play_Walk_Music", gameObject);
    }

    public void startFoodMusic()
    {
        Debug.Log("Starting Food Music");
        AkSoundEngine.PostEvent("Play_Fishing_Music", gameObject);

    }

    public void startWaterMusic()
    {
        Debug.Log("Starting Water Music");
        AkSoundEngine.PostEvent("Play_Water_Music", gameObject);

    }

    public void startGorbageMusic()
    {
        Debug.Log("Starting Gorbage Music");
        AkSoundEngine.PostEvent("Play_Gorbage_Music", gameObject);

    }

    public void startShopMusic()
    {
        Debug.Log("Starting Shop Music");
        AkSoundEngine.PostEvent("Start_Shop_Music", gameObject);
    }
    #endregion
}
