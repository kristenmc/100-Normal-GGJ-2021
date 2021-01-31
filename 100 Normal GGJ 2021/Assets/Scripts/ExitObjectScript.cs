using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitObjectScript : MonoBehaviour
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
        MiniGameManager.MiniGameManagerInstance.deactivateUI();
        GameManager.GameManagerInstance.setMinigameActivity(true);
        GameManager.GameManagerInstance.setCurrentNode(GameManager.GameManagerInstance.getSelectedNode());
        GameManager.GameManagerInstance.setCurrentNodeConnections(GameManager.GameManagerInstance.getSelectedNodeConnections());
        GameManager.GameManagerInstance.updateMapPointer();
    }
}
