using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Game_Manager_Instance.getMinigameActivity())
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                gameObject.transform.Rotate(new Vector3(0, 0, rotationSpeed));
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                gameObject.transform.Rotate(new Vector3(0, 0, -rotationSpeed));
            }
            if (Input.GetButtonDown("Fire1"))
            {
                GameManager.Game_Manager_Instance.spawnInteractables(.1f, .7f, .14f, .05f, .01f, InteractType.Water);
            }
        }
    }
}
