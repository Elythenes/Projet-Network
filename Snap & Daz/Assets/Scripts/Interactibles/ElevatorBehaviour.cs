using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ElevatorBehaviour : MonoBehaviour
{public float timeBetweenFloors = 1;
     public List<float> floorHeights;
     public int floor;
     public bool goingUp = true;
    

    public void MoveElevator()
    {
        if (goingUp)
        {
            transform.DOMoveY(floorHeights[floor+1], timeBetweenFloors);
            floor += 1;
            if (floor == floorHeights.Count - 1)
            {
                goingUp = false;
            }
        }
        else
        {
            transform.DOMoveY(floorHeights[floor-1], timeBetweenFloors);
            floor -= 1;
            if (floor == 0)
            {
                goingUp = true;
            }
        }
    }

}
