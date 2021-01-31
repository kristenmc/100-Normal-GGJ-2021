using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GorbageGame : MonoBehaviour
{

    [SerializeField] int gorbageCount;
    [SerializeField] int badGarbageCount;
    int randomNumber;
    [SerializeField] GameObject badGarbage;
    [SerializeField] GameObject gorbage;

    [SerializeField] int currentBeatMapInt = 0;

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
        
    }

    public void gorbageAI(int beatMapInt)
    {
        currentBeatMapInt = beatMapInt - 48;
        //spawn gorbage
        if(currentBeatMapInt)
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
