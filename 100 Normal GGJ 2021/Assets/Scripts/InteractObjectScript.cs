using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjectScript : MonoBehaviour
{
    [SerializeField] GameObject canvasToOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate()
    {
        canvasToOpen.SetActive(true);
        GameManager.GameManagerInstance.setMinigameActivity(true);
    }
}
