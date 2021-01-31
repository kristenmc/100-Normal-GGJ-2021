using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotation = 0f;
    [SerializeField] float stepsTaken = 0f;
    [SerializeField] float stepsBeforeHunger = 90;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.GameManagerInstance.getMinigameActivity())
        {
            if (Input.GetAxis("Horizontal") > 0 && rotation < 360f)
            {
                gameObject.transform.Rotate(new Vector3(0, 0, rotationSpeed));
                rotation += rotationSpeed;
                stepsTaken += rotationSpeed;
            }
            if (Input.GetAxis("Horizontal") < 0 && rotation > 0f)
            {
                gameObject.transform.Rotate(new Vector3(0, 0, -rotationSpeed));
                rotation -= rotationSpeed;
                stepsTaken += rotationSpeed;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                //GameManager.Game_Manager_Instance.spawnInteractables(.1f, .7f, .14f, .05f, .01f, InteractType.Water);
            }
        }
        if(stepsTaken >= stepsBeforeHunger)
        {
            GameManager.GameManagerInstance.getHungry(1);
            stepsTaken = 0;
        }
    }

    public void resetRotation()
    {
        gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        rotation = 0f;
    }
}
