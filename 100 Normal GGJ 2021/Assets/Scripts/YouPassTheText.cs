using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YouPassTheText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textThing;
    [SerializeField] string input;
    [SerializeField] bool uiBool = false;
    [SerializeField] bool isAnimal = false;
    [SerializeField] bool isGorbage = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if(uiBool)
        {
            if(isAnimal)
            {
                updateText(GameManager.GameManagerInstance.getAnimals());
            }
            else if(isGorbage)
            {
                updateText(GameManager.GameManagerInstance.getGorbageAmt());
            }
        }
    }

    // Update is called once per frame
    public void updateText(int amt)
    {
        textThing.SetText(input + "{0}", amt);
    }

}
