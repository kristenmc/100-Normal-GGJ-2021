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
        GameManager.Game_Manager_Instance.setMinigameActivity(true);
        GameManager.Game_Manager_Instance.setCurrentNode(GameManager.Game_Manager_Instance.getSelectedNode());
        GameManager.Game_Manager_Instance.setCurrentNodeConnections(GameManager.Game_Manager_Instance.getSelectedNodeConnections());
        GameManager.Game_Manager_Instance.updateMapPointer();
    }
}
