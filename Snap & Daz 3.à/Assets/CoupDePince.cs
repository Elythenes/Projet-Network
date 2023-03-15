using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupDePince : MonoBehaviour
{
   public GameObject hitboxCoup;
   public void Coup()
   {
      GameObject hitbox = Instantiate(hitboxCoup, transform.position, Quaternion.identity);
   }
}
