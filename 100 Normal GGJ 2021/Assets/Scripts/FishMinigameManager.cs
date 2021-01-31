using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishMinigameManager : MonoBehaviour
{
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
            if(MiniGameManager.MiniGameManagerInstance.isOnBeat())
            {
                if (timeBeforeClick <= MiniGameManager.MiniGameManagerInstance.getLeeway() && timeBeforeClick >= -MiniGameManager.MiniGameManagerInstance.getLeeway())
                {
                    //add a fish
                    if(fishGettable)
                    {
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
                        //Debug.Log("Got a Fish");
                        fishGettable = false;
                        textCounter.updateText(numFishGot);
                    }
                    else
                    {
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
        timeBeforeClick -= Time.deltaTime;
    }

    public void fishAi(int beatMapInt)
    {
        //Debug.Log("Fish AI: " + beatMapInt);
        currentBeatMapInt = beatMapInt - 48;
        if(currentBeatMapInt == 3)
        {
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
        else if(currentBeatMapInt == 4)
        {
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
        else if(currentBeatMapInt == 5)
        {
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
        else if(currentBeatMapInt == 0)
        {
            if(countFishThree)
            {
                fishThree.GetComponent<RectTransform>().localPosition = pathThree[fishCounter].GetComponent<RectTransform>().localPosition;
                fishCounter++;
                timeBeforeClick = 60f / 120f * (pathThree.Length + 1f - fishCounter);
            }
            else if(countFishFour)
            {
                fishFour.GetComponent<RectTransform>().localPosition = pathFour[fishCounter].GetComponent<RectTransform>().localPosition;
                fishCounter++;
                timeBeforeClick = 60f / 120f * (pathFour.Length + 1f - fishCounter);
            }
            else if(countFishFive)
            {
                fishFive.GetComponent<RectTransform>().localPosition = pathFive[fishCounter].GetComponent<RectTransform>().localPosition;
                fishCounter++;
                timeBeforeClick = 60f / 120f * (pathFive.Length + 1f - fishCounter);
            }
        }
        else if(currentBeatMapInt == 2)
        {
            if(countFishThree)
            {
                fishThree.GetComponent<RectTransform>().position = reticle.GetComponent<RectTransform>().position;
            }
            else if(countFishFour)
            {
                fishFour.GetComponent<RectTransform>().position = reticle.GetComponent<RectTransform>().position;
            }
            else if (countFishFive)
            {
                fishFive.GetComponent<RectTransform>().position = reticle.GetComponent<RectTransform>().position;
            }
            timeBeforeClick = 0f;
            countFishThree = false;
            countFishFour = false;
            countFishFive = false;
            fishCounter = 0;
        }
        else if(currentBeatMapInt == 1)
        {
            fishThree.SetActive(false);
            fishFour.SetActive(false);
            fishFive.SetActive(false);
        }

    }
}
