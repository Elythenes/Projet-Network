using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnapWall : MonoBehaviour
{
   public SnapController snap;
   public GameObject UIinteract;
   public bool canClimb;
   public WallOrientation orientation;
   public bool isUp;
   public SnapWall wall;
   public GameObject emptyDownPos;
public enum WallOrientation
{
   Nord,
   Sud,
   Est,
   Ouest
}

   private void Awake()
   {
      snap = GameObject.Find("Snap").GetComponent<SnapController>();
      wall = GetComponentInParent<SnapWall>();
   }

   private void Update()
   {
      if (canClimb)
      {
         switch (wall.orientation)
         {
            case SnapWall.WallOrientation.Nord :
               emptyDownPos.transform.position = new Vector3(snap.transform.position.x, emptyDownPos.transform.position.y,  emptyDownPos.transform.position.z);
               break;
           /* case SnapWall.WallOrientation.Sud :
               emptyDownPos.transform.position = new Vector3(snap.transform.position.x, emptyDownPos.transform.position.y,  emptyDownPos.transform.position.z);
               break;
            case SnapWall.WallOrientation.Est :
               emptyDownPos.transform.position = new Vector3( emptyDownPos.transform.position.x,  emptyDownPos.transform.position.y,snap.transform.position.z);
               break;*/
            case SnapWall.WallOrientation.Ouest :
               emptyDownPos.transform.position = new Vector3(  emptyDownPos.transform.position.x,  emptyDownPos.transform.position.y,snap.transform.position.z);
               break;
         }
         if (Input.GetKeyDown(KeyCode.F))
         {
            if (snap.isWalled)
            {
               snap.isWalled = false;
            }
            else
            {
               if (!isUp)
               {
                  switch (wall.orientation)
                  {
                     case SnapWall.WallOrientation.Nord :
                        snap.transform.rotation = new Quaternion();
                        break;
                  
                     case SnapWall.WallOrientation.Ouest :
                        snap.transform.rotation = new Quaternion(-90,0,90,0);
                        break;
                  }
               }
               
               snap.isWalled = true;  
               if (isUp)
               {
                  switch (wall.orientation)
                  {
                     case SnapWall.WallOrientation.Nord :
                        snap.transform.rotation = new Quaternion();
                        break;
                  
                     case SnapWall.WallOrientation.Ouest :
                        snap.transform.rotation = new Quaternion(90,90,180,0);
                        break;
                  }
                  
                  snap.transform.position = emptyDownPos.transform.position;
               }
            }
            
         }
      }
   }


   public void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.layer == 7)
      {
         switch (orientation)
         {
            case WallOrientation.Nord:
               snap.wallOrientation = 1;
               break;
            case WallOrientation.Sud:
               snap.wallOrientation = 3;
               break;
            case WallOrientation.Est:
               snap.wallOrientation = 2;
               break;
            case WallOrientation.Ouest:
               snap.wallOrientation = 4;
               break;
         }
         UIinteract.SetActive(true);
         canClimb = true;
      }
    
   }
   
   public void OnTriggerExit(Collider other)
   {
      if (other.gameObject.layer == 7)
      {
         UIinteract.SetActive(false);
         canClimb = false;
      }
   }
}
