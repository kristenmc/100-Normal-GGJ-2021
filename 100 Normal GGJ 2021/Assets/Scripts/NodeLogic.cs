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
        connections = transform.parent.gameObject.GetComponent<NodeConnection>().getConnections();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nodeClick()
    {
        foreach (GameObject node in GameManager.Game_Manager_Instance.getCurrentNodeConnections())
        {
            if (node == gameObject && !alreadyExplored)
            {
                GameManager.Game_Manager_Instance.spawnInteractables(generationWeights[0], generationWeights[1], 
                    generationWeights[2], generationWeights[3], generationWeights[4], pity);
                GameManager.Game_Manager_Instance.setSelectedNode(gameObject);
                GameManager.Game_Manager_Instance.setSelectedNodeConnections(connections);
                alreadyExplored = true;
            }
        }
    }
}
