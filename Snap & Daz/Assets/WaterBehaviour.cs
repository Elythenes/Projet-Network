using System;
using UnityEngine;
using UnityEngine.Rendering;

public class WaterBehaviour : MonoBehaviour
{
   public Volume gVolume;

   private void Start()
   {
      gVolume = GameObject.Find("Water Global Volume").GetComponent<Volume>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("MainCamera"))
      {
         gVolume.weight = 1;
      }
   }
   
   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("MainCamera"))
      {
         gVolume.weight = 0;
      }
   }
}
