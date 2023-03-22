using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactible
{
   [Header("Interaction")] 
   [Tooltip("Glissez tous les éléments qui sont activés par le levier")] public List<Activable> activatedElements;

   [Header("Cooldown")]
   [Tooltip("Détermine le temps avant que le levier puisse à nouveau être utilisé")] public float cooldown;

   public override void Interact()
   {
      if (!canBeInteracted) return;
      
      base.Interact();

      foreach (var obj in activatedElements)
      {
         if (isInteracted)
         {
            obj.Activate();
         }
         else
         {
            obj.Desactivate();
         }
      }

      StartCoroutine(Cooldown());
   }

   public override void StopInteract()
   {
      
   }

   private IEnumerator Cooldown()
   {
      yield return new WaitForSeconds(cooldown);

      canBeInteracted = true;
   }
}
