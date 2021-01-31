using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YouPassTheText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textThing;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void updateText(int amt)
    {
        textThing.SetText("Current Score: {0}", amt);
    }

}
