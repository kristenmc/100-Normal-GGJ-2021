using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] Image Foodbar;
    [SerializeField] float resourceNum;
    [SerializeField] float resourceMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Foodbar.fillAmount = resourceNum / resourceMax;
    }
}
