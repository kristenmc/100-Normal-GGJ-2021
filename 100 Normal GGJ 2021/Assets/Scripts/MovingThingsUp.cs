using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingThingsUp : MonoBehaviour
{

    [SerializeField] GameObject MoveableObject;
    [SerializeField] float moveDistance;
    [SerializeField] float moveTime;

    void MoveTheObject()
    {
        LeanTween.moveY(MoveableObject, moveDistance, moveTime);
        LeanTween.moveY(MoveableObject, -moveDistance, moveTime).setDelay(.1f);
    }

}
