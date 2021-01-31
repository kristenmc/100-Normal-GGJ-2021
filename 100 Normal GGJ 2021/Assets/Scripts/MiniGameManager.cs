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

    [SerializeField] int BPM;
    [SerializeField] InteractType currentMiniGameName;
    [SerializeField] float timer;
    [SerializeField] float leeWay;

    #endregion

    [SerializeField] AK.Wwise.Event shopMusicStart;
    [SerializeField] AK.Wwise.Event walkMusicStart;
    [SerializeField] AK.Wwise.Event fishingMusicStart;
    [SerializeField] AK.Wwise.Event gorbageMusicStart;
    [SerializeField] AK.Wwise.Event waterMusicStart;


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
    private void Start()
    {
        MiniGameManager.MiniGameManagerInstance.onBeatCall += BeatReset;
    }

    void BeatReset(int Throwaway)
    {
        timer = 0;
    }

    public bool isOnBeat()
    {
        if (timer <= leeWay)
        {
            return true;
        }
        else if (timer >= 60f / 120f - leeWay && currentMiniGameName == InteractType.Food)
        {
            return true;
        }
        else if (timer >= 60f / 132f - leeWay && currentMiniGameName == InteractType.Water)
        {
            return true;
        }
        else if (timer >= 60f / 124f - leeWay && currentMiniGameName == InteractType.Gorbage)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float getLeeway()
    {
        return leeWay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            startFoodMinigame();
        }
        timer += Time.deltaTime;
    }

    #region Start Minigame
    public void startFoodMinigame()
    {
        if (!allowForCallbacks)
        {
            foodCanvas.SetActive(true);
            foodCanvas.GetComponent<FishMinigameManager>().resetNumFishGot();
            GameManager.GameManagerInstance.setMinigameActivity(true);
            pauseMusic();
            startFoodMusic();
            resumeFoodMusic();
            generatedBeatMap = gameObject.GetComponent<BeatMapMaker>().GenerateBeatMap(foodBeatMapLength, foodBeatMapParts);
            allowForCallbacks = true;
            currentBeatmapLocation = 0;
            currentMiniGameName = InteractType.Food;
            //temp func for testing callback logic
            MiniGameManagerInstance.onBeatCall += callBackTest;
        }
    }

    public void startWaterMinigame()
    {
        if (!allowForCallbacks)
        {
            waterCanvas.SetActive(true);
            GameManager.GameManagerInstance.setMinigameActivity(true);
            pauseMusic();
            startWaterMusic();
            generatedBeatMap = gameObject.GetComponent<BeatMapMaker>().GenerateBeatMap(waterBeatMapLength, waterBeatMapParts);
            allowForCallbacks = true;
            currentBeatmapLocation = 0;
            currentMiniGameName = InteractType.Water;
            //temp func for testing callback logic
            MiniGameManagerInstance.onBeatCall += callBackTest;
        }
    }

    public void startGorbageMinigame()
    {
        if (!allowForCallbacks)
        {
            gorbageCanvas.SetActive(true);
            GameManager.GameManagerInstance.setMinigameActivity(true);
            pauseMusic();
            startGorbageMusic();
            generatedBeatMap = gameObject.GetComponent<BeatMapMaker>().GenerateBeatMap(gorbageBeatMapLength, gorbageBeatMapParts);
            allowForCallbacks = true;
            currentBeatmapLocation = 0;
            currentMiniGameName = InteractType.Gorbage;
            //temp func for testing callback logic
            MiniGameManagerInstance.onBeatCall += callBackTest;
        }
    }

    public void startShopInteraction()
    {
        if (!allowForCallbacks)
        {
        }
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
                //Debug.Log("Current Beat on Beatmap: " + generatedBeatMap[currentBeatmapLocation]);
                MiniGameManagerInstance.onBeatCall(generatedBeatMap[currentBeatmapLocation]);
            }
            currentBeatmapLocation++;
        }
    }

    public void callBackTest(int i)
    {
        //Debug.Log(generatedBeatMap[currentBeatmapLocation] + " should be equal to: " + i);
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
        walkMusicStart.Post(gameObject);
    }

    public void startFoodMusic()
    {
        Debug.Log("Starting Food Music");
        fishingMusicStart.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBeat | (uint)AkCallbackType.AK_MusicSyncExit | (uint)AkCallbackType.AK_MusicSyncEntry, Callback_Function);

    }

    public void startWaterMusic()
    {
        Debug.Log("Starting Water Music");
        waterMusicStart.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBeat | (uint)AkCallbackType.AK_MusicSyncExit | (uint)AkCallbackType.AK_MusicSyncEntry, Callback_Function);

    }

    public void startGorbageMusic()
    {
        Debug.Log("Starting Gorbage Music");
        gorbageMusicStart.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBeat | (uint)AkCallbackType.AK_MusicSyncExit | (uint)AkCallbackType.AK_MusicSyncEntry, Callback_Function);

    }

    public void startShopMusic()
    {
        Debug.Log("Starting Shop Music");
        shopMusicStart.Post(gameObject);
    }
    #endregion
}
