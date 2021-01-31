using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjectScript : MonoBehaviour
{
    [SerializeField] InteractType type;
    [SerializeField] bool alreadyInteractedWith = false;
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
        if(!alreadyInteractedWith)
        {
            MiniGameManager.MiniGameManagerInstance.chooseMinigame(type);
            alreadyInteractedWith = true;
        }
    }
}
