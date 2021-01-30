using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnection : MonoBehaviour
{
    [SerializeField] GameObject[] connectionsList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] getConnections()
    {
        return connectionsList;
    }

    public void callChildFunc()
    {
        gameObject.GetComponentInChildren<NodeLogic>().nodeClick();
    }
}
