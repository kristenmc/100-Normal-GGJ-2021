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
            resourceNum = GameManager.Game_Manager_Instance.getFoodAmt();
            resourceMax = GameManager.Game_Manager_Instance.getMaxFood();
        }
        if (resourceType == "Water")
        {
            resourceNum = GameManager.Game_Manager_Instance.getWaterAmt();
            resourceMax = GameManager.Game_Manager_Instance.getMaxWater();
        }
        Foodbar.fillAmount = resourceNum / resourceMax;
    }
}
