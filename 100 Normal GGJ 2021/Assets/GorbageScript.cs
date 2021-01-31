using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorbageScript : MonoBehaviour
{
    [SerializeField] float lerpTime;
    [SerializeField] float maxLerpTime = 60f / 124f * 7;
    [SerializeField] RectTransform despawnLocation;
    [SerializeField] RectTransform spawnLocation;

    [SerializeField] bool isActive;
    [SerializeField] bool isYeet;
    [SerializeField] float yeetTime = .1f;
    [SerializeField] float maxYeetTime = .1f;
    [SerializeField] RectTransform yeetStart;
    [SerializeField] RectTransform yeetEnd;


    // Start is called before the first frame update
    void Start()
    {
        lerpTime = maxLerpTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && !isYeet)
        {
            lerpTime -= Time.deltaTime;
            RectTransform garbageRect = gameObject.GetComponent<RectTransform>();
            garbageRect.localPosition = Vector2.Lerp(despawnLocation.localPosition,
                spawnLocation.localPosition,
                lerpTime / maxLerpTime);
            if (gameObject.GetComponent<RectTransform>().localPosition == despawnLocation.localPosition)
            {
                Debug.Log("adjkfjasdlkfjahdslkfjh");
                isActive = false;
            }
        }
        else if(isYeet)
        {
            yeetTime -= Time.deltaTime;
            RectTransform garbageRect = gameObject.GetComponent<RectTransform>();
            garbageRect.localPosition = Vector2.Lerp(yeetEnd.localPosition,
                yeetStart.localPosition,
                yeetTime / maxYeetTime);
            if(gameObject.GetComponent<RectTransform>().localPosition == yeetEnd.localPosition)
            {
                isActive = false;
                isYeet = false;
            }
        }
    }

    public bool getActivity()
    {
        return isActive;
    }

    public void resetGorbagePosition()
    {
        if(!isActive)
        {
            gameObject.GetComponent<RectTransform>().localPosition = spawnLocation.localPosition;
            isActive = true;
            fixLerpTimer(maxLerpTime);
        }
    }

    public void fixLerpTimer(float correctTime)
    {
        lerpTime = correctTime;
    }

    public void yeet(RectTransform end)
    {
        yeetStart = gameObject.GetComponent<RectTransform>();
        isYeet = true;
        yeetEnd = end;
        yeetTime = maxYeetTime;
    }
}
