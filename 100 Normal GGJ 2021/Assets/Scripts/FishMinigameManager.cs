using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishMinigameManager : MonoBehaviour
{
    [SerializeField] bool gameStarted = false;
    [SerializeField] GameObject fishThree;
    [SerializeField] GameObject fishFour;
    [SerializeField] GameObject fishFive;
    [SerializeField] GameObject currentFish;
    [SerializeField] Transform[] pathThree;
    [SerializeField] Transform[] pathFour;
    [SerializeField] Transform[] pathFive;
    [SerializeField] Transform reticle;
    [SerializeField] int currentBeatMapInt;
    [SerializeField] int fishCounter = 0;
    [SerializeField] bool countFishThree = false;
    [SerializeField] bool countFishFour = false;
    [SerializeField] bool countFishFive = false;
    [SerializeField] float timeBeforeClick = 0f;
    [SerializeField] bool fishGettable = false;

    [SerializeField] int numFishGot = 0;
    [SerializeField] YouPassTheText textCounter;

    // Start is called before the first frame update
    void Start()
    {
        MiniGameManager.MiniGameManagerInstance.onBeatCall += fishAi;
    }

    public void resetNumFishGot()
    {
        numFishGot = 0;
        textCounter.updateText(numFishGot);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            AkSoundEngine.PostEvent("Play_Claw_Swipe", gameObject);
            if(MiniGameManager.MiniGameManagerInstance.isOnBeat())
            {
                if (timeBeforeClick <= MiniGameManager.MiniGameManagerInstance.getLeeway() && timeBeforeClick >= -MiniGameManager.MiniGameManagerInstance.getLeeway())
                {
                    //add a fish
                    if(fishGettable)
                    {
                        AkSoundEngine.PostEvent("Play_Fish_Caught", gameObject);
                        int tempfish = 1;
                        for(int i = 0; i < GameManager.GameManagerInstance.getNetsAmt(); i++)
                        {
                            float randomNum = Random.Range(0.0f, 1.0f);
                            if (randomNum <= GameManager.GameManagerInstance.getItemMult())
                            {
                                tempfish++;
                            }
                        }
                        numFishGot += tempfish;
                        GameManager.GameManagerInstance.changeFood(tempfish);
                        GameManager.GameManagerInstance.changeWater(tempfish);
                        //Debug.Log("Got a Fish");
                        fishGettable = false;
                        textCounter.updateText(numFishGot);
                    }
                    else
                    {
                        AkSoundEngine.PostEvent("Play_Miss", gameObject);
                        Debug.Log("Missed the Fish");
                    }
                }
                else if(timeBeforeClick > MiniGameManager.MiniGameManagerInstance.getLeeway())
                {
                    fishGettable = false; 
                    currentFish.GetComponent<Image>().color = new Color(255, 0, 0, 100);
                }
                //play close hand animation
            }
        }
        if(gameStarted)
        {
            timeBeforeClick -= Time.deltaTime;
        }
        if (timeBeforeClick <= -5)
        {
            AkSoundEngine.PostEvent("Reset_Fish_Cues", gameObject);
            gameStarted = false;
            timeBeforeClick = 300;
            MiniGameManager.MiniGameManagerInstance.endMinigame();
        }
    }

    public void fishAi(int beatMapInt)
    {
        //Debug.Log("Fish AI: " + beatMapInt);
        currentBeatMapInt = beatMapInt - 48;
        //spawn fish 3
        if(currentBeatMapInt == 3)
        {
            AkSoundEngine.PostEvent("Play_Fish3_Cues", gameObject);
            countFishThree = true;
            fishCounter = 0;
            fishThree.SetActive(true);
            fishFour.SetActive(false);
            fishFive.SetActive(false);
            currentFish = fishThree;
            currentFish.GetComponent<Image>().color = new Color(255, 255, 255, 100);
            fishThree.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            timeBeforeClick = 60f / 120f * (pathThree.Length + 1f);
            fishGettable = true;
        }
        //spawn fish 4
        else if (currentBeatMapInt == 4)
        {
            AkSoundEngine.PostEvent("Play_Fish4_Cues", gameObject);
            countFishFour = true;
            fishCounter = 0;
            fishThree.SetActive(false);
            fishFour.SetActive(true);
            fishFive.SetActive(false);
            currentFish = fishFour;
            currentFish.GetComponent<Image>().color = new Color(255, 255, 255, 100);
            fishFour.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            timeBeforeClick = 60f / 120f * (pathFour.Length + 1f);
            fishGettable = true;
        }
        //spawn fish 5
        else if (currentBeatMapInt == 5)
        {
            AkSoundEngine.PostEvent("Play_Fish5_Cues", gameObject);
            countFishFive = true;
            fishCounter = 0;
            fishThree.SetActive(false);
            fishFour.SetActive(false);
            fishFive.SetActive(true);
            currentFish = fishFive;
            currentFish.GetComponent<Image>().color = new Color(255, 255, 255, 100);
            fishFive.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            timeBeforeClick = 60f / 120f * (pathFive.Length + 1f);
            fishGettable = true;
        }
        //move all fish
        else if(currentBeatMapInt == 0)
        {
            //fish 3 movement
            if(countFishThree)
            {
                AkSoundEngine.PostEvent("Play_Fish3_Cues", gameObject);
                fishThree.GetComponent<RectTransform>().localPosition = pathThree[fishCounter].GetComponent<RectTransform>().localPosition;
                fishCounter++;
                timeBeforeClick = 60f / 120f * (pathThree.Length + 1f - fishCounter);
            }
            //fish 4 movement
            else if(countFishFour)
            {
                AkSoundEngine.PostEvent("Play_Fish4_Cues", gameObject);
                fishFour.GetComponent<RectTransform>().localPosition = pathFour[fishCounter].GetComponent<RectTransform>().localPosition;
                fishCounter++;
                timeBeforeClick = 60f / 120f * (pathFour.Length + 1f - fishCounter);
            }
            //fish 5 movement
            else if(countFishFive)
            {
                AkSoundEngine.PostEvent("Play_Fish5_Cues", gameObject);
                fishFive.GetComponent<RectTransform>().localPosition = pathFive[fishCounter].GetComponent<RectTransform>().localPosition;
                fishCounter++;
                timeBeforeClick = 60f / 120f * (pathFive.Length + 1f - fishCounter);
            }
        }
        //final movement the fish makes (enters the paw/reticle)
        else if(currentBeatMapInt == 2)
        {
            if(countFishThree)
            {
                AkSoundEngine.PostEvent("Play_Fish3_Cues", gameObject);
                fishThree.GetComponent<RectTransform>().position = reticle.GetComponent<RectTransform>().position;
            }
            else if(countFishFour)
            {
                AkSoundEngine.PostEvent("Play_Fish4_Cues", gameObject);
                fishFour.GetComponent<RectTransform>().position = reticle.GetComponent<RectTransform>().position;
            }
            else if (countFishFive)
            {
                AkSoundEngine.PostEvent("Play_Fish5_Cues", gameObject);
                fishFive.GetComponent<RectTransform>().position = reticle.GetComponent<RectTransform>().position;
            }
            timeBeforeClick = 0f;
            countFishThree = false;
            countFishFour = false;
            countFishFive = false;
            fishCounter = 0;
        }
        //reset fish
        else if(currentBeatMapInt == 1)
        {
            fishThree.SetActive(false);
            fishFour.SetActive(false);
            fishFive.SetActive(false);
        }

    }

    public void startGame()
    {
        gameStarted = true;
    }
}
