using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    [SerializeField] Canvas shopCanvas;

    public void EnableShop()
    {
        shopCanvas.enabled = true;
    }

}
