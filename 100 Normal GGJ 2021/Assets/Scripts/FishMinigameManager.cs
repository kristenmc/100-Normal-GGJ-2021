using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMinigameManager : MonoBehaviour
{
    [SerializeField] GameObject fishThree;
    [SerializeField] GameObject fishFour;
    [SerializeField] GameObject fishFive;
    [SerializeField] Transform[] pathThree;
    [SerializeField] Transform[] pathFour;
    [SerializeField] Transform[] pathFive;
    [SerializeField] Transform reticle;
    [SerializeField] int fishCounter = 0;
    [SerializeField] bool countFishThree = false;
    [SerializeField] bool countFishFour = false;
    [SerializeField] bool countFishFive = false;

    // Start is called before the first frame update
    void Start()
    {
        MiniGameManager.MiniGameManagerInstance.onBeatCall += fishAi;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fishAi(int beatMapInt)
    {
        Debug.Log("Fish AI: " + beatMapInt);
        beatMapInt -= 48;
        if(beatMapInt == 3)
        {
            countFishThree = true;
            fishCounter = 0;
            fishThree.SetActive(true);
            fishFour.SetActive(false);
            fishFive.SetActive(false);
            fishThree.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
        else if(beatMapInt == 4)
        {
            countFishFour = true;
            fishCounter = 0;
            fishThree.SetActive(false);
            fishFour.SetActive(true);
            fishFive.SetActive(false);
            fishFour.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
        else if(beatMapInt == 5)
        {
            countFishFive = true;
            fishCounter = 0;
            fishThree.SetActive(false);
            fishFour.SetActive(false);
            fishFive.SetActive(true);
            fishFive.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
        else if(beatMapInt == 0)
        {
            if(countFishThree)
            {
                fishThree.GetComponent<RectTransform>().localPosition = pathThree[fishCounter].GetComponent<RectTransform>().localPosition;
                fishCounter++;
            }
            else if(countFishFour)
            {
                fishFour.GetComponent<RectTransform>().localPosition = pathFour[fishCounter].GetComponent<RectTransform>().localPosition;
                fishCounter++;
            }
            else if(countFishFive)
            {
                fishFive.GetComponent<RectTransform>().localPosition = pathFive[fishCounter].GetComponent<RectTransform>().localPosition;
                fishCounter++;
            }
        }
        else if(beatMapInt == 2)
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
            countFishThree = false;
            countFishFour = false;
            countFishFive = false;
            fishCounter = 0;
        }
        else if(beatMapInt == 1)
        {
            fishThree.SetActive(false);
            fishFour.SetActive(false);
            fishFive.SetActive(false);
        }

    }
}
