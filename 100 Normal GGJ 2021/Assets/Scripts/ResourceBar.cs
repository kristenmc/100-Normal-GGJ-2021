using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] Image Foodbar;
    float resourceNum;
    float resourceMax;
    [SerializeField] string resourceType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (resourceType == "Food")
        {
            resourceNum = GameManager.GameManagerInstance.getFoodAmt();
            resourceMax = GameManager.GameManagerInstance.getMaxFood();
        }
        if (resourceType == "Water")
        {
            resourceNum = GameManager.GameManagerInstance.getWaterAmt();
            resourceMax = GameManager.GameManagerInstance.getMaxWater();
        }
        Foodbar.fillAmount = resourceNum / resourceMax;
    }
}
