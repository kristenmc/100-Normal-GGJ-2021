using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] bool canInteract = false;
    [SerializeField] Collider2D interactCollision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetButtonDown("Fire1"))
        {
            activateInteractable();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        canInteract = true;
        interactCollision = other;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canInteract = false;
        interactCollision = null;
    }

    public void activateInteractable()
    {
        if(canInteract && interactCollision.tag == "Interactable")
        {
            interactCollision.gameObject.GetComponent<InteractObjectScript>().activate();
            canInteract = false;
        }
        else if(canInteract && interactCollision.tag == "Exit")
        {
            interactCollision.gameObject.GetComponent<ExitObjectScript>().activate();
            canInteract = false;
        }
    }
}
