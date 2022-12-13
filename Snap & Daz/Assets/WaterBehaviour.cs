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
      Debug.Log("oui");
      if (other.CompareTag("MainCamera"))
      {
         Debug.Log("ouiiiiiiiiiii");
         gVolume.weight = 1;
      }
   }
   
   private void OnTriggerExit(Collider other)
   {
      Debug.Log("oui");
      if (other.CompareTag("MainCamera"))
      {
         Debug.Log("ouiiiiiiiiiii");
         gVolume.weight = 0;
      }
   }
}
