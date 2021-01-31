using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GorbageGame : MonoBehaviour
{
    [SerializeField] bool gameStarted = false;
    [SerializeField] int gorbageCount;
    [SerializeField] int badGarbageCount;
    [SerializeField] GorbageScript[] spawnedGarbage;
    [SerializeField] GorbageScript[] spawnedGorbage;

    [SerializeField] int currentBeatMapInt = 0;
    [SerializeField] RectTransform spawnLocation;
    [SerializeField] RectTransform despawnLocation;
    //[SerializeField] float[] lerpTime;
    [SerializeField] float baseLerpTime = 60f / 124f;
    [SerializeField] GameObject selectedGarbage;
    [SerializeField] bool isGorbage = false;
    [SerializeField] float timeBeforeClick = 0f;
    [SerializeField] bool itemGettable = false;
    [SerializeField] RectTransform rightYeet;
    [SerializeField] RectTransform leftYeet;

    [SerializeField] YouPassTheText textCounterGorbage;
    [SerializeField] YouPassTheText textCounterBadGarbage;


    // Start is called before the first frame update
    void Start()
    {
        MiniGameManager.MiniGameManagerInstance.onBeatCall += gorbageAI;
    }
    
    public void resetNumGorbageGot()
    {
        gorbageCount = 0;
        badGarbageCount = 0;
        textCounterGorbage.updateText(gorbageCount);
        textCounterBadGarbage.updateText(badGarbageCount);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(gameStarted)
        {
            timeBeforeClick -= Time.deltaTime;
        }
        if(timeBeforeClick <= -5)
        {
            gameStarted = false;
            timeBeforeClick = 3 * baseLerpTime;
            MiniGameManager.MiniGameManagerInstance.endMinigame();
        }
        /*
        for(int i = 0; i < spawnedGarbage.Length; i++)
        {
            if (spawnedGarbage[i] != null)
            {
                lerpTime[i] -= Time.deltaTime;
                RectTransform garbageRect = spawnedGarbage[i].GetComponent<RectTransform>();
                garbageRect.localPosition = Vector2.Lerp(despawnLocation.localPosition,
                    spawnLocation.localPosition,
                    lerpTime[i]/maxLerpTime);
                if (garbageRect.localPosition == despawnLocation.localPosition)
                {
                    //Destroy(garbageRect.gameObject);
                }
            }
        }
        */
        //Left input for Garbage
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (MiniGameManager.MiniGameManagerInstance.isOnBeat())
            {
                if (timeBeforeClick <= MiniGameManager.MiniGameManagerInstance.getLeeway() && timeBeforeClick >= -MiniGameManager.MiniGameManagerInstance.getLeeway())
                {
                    if(itemGettable)
                    {
                        selectedGarbage.GetComponent<GorbageScript>().yeet(leftYeet);
                        if (!isGorbage)
                        {
                            itemGettable = false;
                            badGarbageCount++;
                            textCounterBadGarbage.updateText(badGarbageCount);
                            //add 1 to garbage counter
                        }
                        else if(isGorbage)
                        {
                            itemGettable = false;
                            selectedGarbage.GetComponent<Image>().color = new Color(255, 0, 0, 100);
                            //mistake notification
                        }

                    }
                }
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if (MiniGameManager.MiniGameManagerInstance.isOnBeat())
            {
                if (timeBeforeClick <= MiniGameManager.MiniGameManagerInstance.getLeeway() && timeBeforeClick >= -MiniGameManager.MiniGameManagerInstance.getLeeway())
                {
                    if (itemGettable)
                    {
                        selectedGarbage.GetComponent<GorbageScript>().yeet(rightYeet);
                        if (isGorbage)
                        {
                            itemGettable = false;
                            int tempGorbage = 1;
                            for (int i = 0; i < GameManager.GameManagerInstance.getDetectorAmt(); i++)
                            {
                                float randomNum = Random.Range(0.0f, 1.0f);
                                if(randomNum <= GameManager.GameManagerInstance.getItemMult())
                                {
                                    tempGorbage++;
                                }
                            }
                            gorbageCount += tempGorbage;
                            textCounterGorbage.updateText(gorbageCount);
                        }
                        else if (!isGorbage)
                        {
                            itemGettable = false;
                            gorbageCount--;
                            textCounterGorbage.updateText(gorbageCount);
                            selectedGarbage.GetComponent<Image>().color = new Color(255, 0, 0, 100);
                            //mistake notification
                        }
                    }
                }
            }
            else
            {
                itemGettable = false;
                selectedGarbage.GetComponent<Image>().color = new Color(255, 0, 0, 100);
                //mistake notification
            }
        }
    }

    public void gorbageAI(int beatMapInt)
    {
        currentBeatMapInt = beatMapInt - 48;
        //spawn gorbage
        if(currentBeatMapInt == 3)
        {
            selectedGarbage = findInactiveGorbage();
            if(selectedGarbage != null)
            {
                selectedGarbage.GetComponent<GorbageScript>().resetGorbagePosition();
                isGorbage = true;
                timeBeforeClick = 3 * baseLerpTime;
                itemGettable = true;
                selectedGarbage.GetComponent<Image>().color = new Color(255, 255, 255, 100);
            }
        }
        //spawn bad garbage
        else if(currentBeatMapInt == 4)
        {
            selectedGarbage = findInactiveGarbage();
            if (selectedGarbage != null)
            {
                selectedGarbage.GetComponent<GorbageScript>().resetGorbagePosition();
                isGorbage = false;
                timeBeforeClick = 3 * baseLerpTime;
                itemGettable = true;
                selectedGarbage.GetComponent<Image>().color = new Color(255, 255, 255, 100);
            }
        }
        else if(currentBeatMapInt == 1)
        {
            if (selectedGarbage != null)
            {
                selectedGarbage.GetComponent<GorbageScript>().fixLerpTimer(baseLerpTime * 6);
                timeBeforeClick = 2 * baseLerpTime;
            }
        }
        else if(currentBeatMapInt == 5)
        {
            if (selectedGarbage != null)
            {
                selectedGarbage.GetComponent<GorbageScript>().fixLerpTimer(baseLerpTime * 5);
                timeBeforeClick = baseLerpTime;
            }
        }
        //allow for collection
        else if(currentBeatMapInt == 2)
        {
            timeBeforeClick = 0f;
            if(selectedGarbage != null)
            {
                selectedGarbage.GetComponent<GorbageScript>().fixLerpTimer(baseLerpTime * 4);
            }

        }
    }

    public GameObject findInactiveGorbage()
    {
        foreach (GorbageScript gorbage in spawnedGorbage)
        {
            if (!gorbage.getActivity())
            {
                return gorbage.gameObject;
            }
        }
        return null;
    }

    public GameObject findInactiveGarbage()
    {
        foreach (GorbageScript garbage in spawnedGarbage)
        {
            if(!garbage.getActivity())
            {
                return garbage.gameObject;
            }
        }
        return null;
    }

    public void startGame()
    {
        gameStarted = true;
    }

    void GorbageTick()
    {
        /*
        randomNumber = Random.Range(0, 1);
        if (randomNumber == 0)
        {
            gorbageImage.enabled = true;
            
        }
        else if (randomNumber == 1)
        {
            badGarbageImage.enabled = true;
        }*/
    }

    void GorbageDecide()
    {
        /*
        if (Input.GetKeyDown(KeyCode.LeftArrow) && randomNumber == 0)
        {
            badGarbageCount++;
            LeanTween.moveX(gorbageImage.rectTransform, 736, .3f);
            gorbageImage.enabled = false;
            gorbageImage.rectTransform.position = new Vector3(0, 0, 0);
            //I dont know how to play the animation and set it back to 0 in a good way
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && randomNumber == 0)
        {
            gorbageCount++;
            LeanTween.moveX(badGarbageImage.rectTransform, 736, .3f);
            gorbageImage.enabled = false;
            badGarbageImage.rectTransform.position = new Vector3(0, 0, 0);
            //I dont know how to play the animation and set it back to 0 in a good way
        }*/
    }

}
