using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] Canvas PauseMenu;
    [SerializeField] public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PauseGame()
    {
        PauseMenu.enabled = true;
        isPaused = true;
    }
    public void ResumeGame()
    {
        PauseMenu.enabled = false;
        isPaused = false;
    }
}
