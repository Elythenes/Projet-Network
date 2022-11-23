using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnapWall : MonoBehaviour
{
   public LayerMask SnapLayer;
   public CrabeController snap;

   public bool canClimb;
   private void Awake()
   {
      snap = GameObject.Find("SNAP").GetComponent<CrabeController>();
   }

   private void Update()
   {
      if (canClimb)
      {
         if (Input.GetKeyDown(KeyCode.A))
         {
            snap.isWalled = true;
         }
      }
   }


   public void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.layer == 7)
      {
         Debug.Log("rentré");
         canClimb = true;
      }
    
   }
   
   public void OnTriggerExit(Collider other)
   {
      if (other.gameObject.layer == 7)
      {
         canClimb = false;
      }
   }
}
