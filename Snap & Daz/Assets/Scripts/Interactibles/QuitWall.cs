using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitWall : MonoBehaviour
{
   public SnapController snap;
   public GameObject UIinteract;
   public bool canClimb;
   public GameObject emptyClimbPos;
   public SnapWall wall;
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
                  emptyClimbPos.transform.position = new Vector3(snap.transform.position.x, emptyClimbPos.transform.position.y,  emptyClimbPos.transform.position.z);
                  break;
               case SnapWall.WallOrientation.Sud :
                  emptyClimbPos.transform.position = new Vector3(snap.transform.position.x, emptyClimbPos.transform.position.y,  emptyClimbPos.transform.position.z);
                  break;
               case SnapWall.WallOrientation.Est :
                  emptyClimbPos.transform.position = new Vector3( emptyClimbPos.transform.position.x,  emptyClimbPos.transform.position.y,snap.transform.position.z);
                  break;
               case SnapWall.WallOrientation.Ouest :
                  emptyClimbPos.transform.position = new Vector3(  emptyClimbPos.transform.position.x,  emptyClimbPos.transform.position.y,snap.transform.position.z);
                  break;
            }
            
            if (Input.GetKeyDown(KeyCode.F))
            {
               snap.transform.position = emptyClimbPos.transform.position;
               snap.isWalled = false;
            }
         }
      }
   
   
      public void OnTriggerEnter(Collider other)
      {
         if (other.gameObject.layer == 7)
         {
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
