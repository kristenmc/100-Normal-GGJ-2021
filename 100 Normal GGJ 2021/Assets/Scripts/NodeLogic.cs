using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLogic : MonoBehaviour
{
    [SerializeField] float[] generationWeights;
    [SerializeField] InteractType pity;
    [SerializeField] GameObject[] connections;
    [SerializeField] bool alreadyExplored = false;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nodeClick()
    {
        foreach (GameObject node in GameManager.GameManagerInstance.getCurrentNodeConnections())
        {
            if (node == transform.parent.gameObject && !alreadyExplored)
            {
                GameManager.GameManagerInstance.spawnInteractables(generationWeights[0], 
                    generationWeights[1], 
                    generationWeights[2], 
                    generationWeights[3], 
                    generationWeights[4], 
                    pity);
                GameManager.GameManagerInstance.setSelectedNode(gameObject);
                GameManager.GameManagerInstance.setSelectedNodeConnections(connections);
                alreadyExplored = true;
                Debug.Log("Generated" + pity + "type Interactables with the Following Weights: \n Food: " + generationWeights[0] + 
                    "\n Water: " + generationWeights[1] + 
                    "\n Gorbage: " + generationWeights[2] +
                    "\n Shop: " + generationWeights[3] +
                    "\n Animal: " + generationWeights[4]);
                GameManager.GameManagerInstance.resetEarthRotation();
                GameManager.GameManagerInstance.closeMap();
                GameManager.GameManagerInstance.setMinigameActivity(false);
            }
        }
        MiniGameManager.MiniGameManagerInstance.activateUI();
    }

    public void getParentConnections()
    {
        connections = transform.parent.gameObject.GetComponent<NodeConnection>().getConnections();

    }
}
