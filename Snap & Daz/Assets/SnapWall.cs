using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnapWall : MonoBehaviour
{
   public LayerMask SnapLayer;
   public CrabeController snap;

   private void Awake()
   {
      snap = GameObject.Find("SNAP").GetComponent<CrabeController>();
   }

   public void OnTriggerStay(Collider other)
   {
      //if (other.gameObject.layer == SnapLayer)
      //{
         Debug.Log("rentré");
         if(Input.GetKeyDown(KeyCode.A))
         {
            Debug.Log("appuyé");
            snap.isWalled = true;
         }
      //}
   }
}
