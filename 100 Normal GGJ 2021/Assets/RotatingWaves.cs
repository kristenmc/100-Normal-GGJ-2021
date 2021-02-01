using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWaves : MonoBehaviour
{
    [SerializeField] Transform[] waves;
    [SerializeField] float[] waveSpeeds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].Rotate(new Vector3(0, 0, waveSpeeds[i]));
        }
    }
}
