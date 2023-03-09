using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactible
{
   [Header("Interaction")] 
   public List<Activable> activatedElements;

   [Header("Cooldown")]
   public float cooldown;

   public override void Interact()
   {
      if (!canBeInteracted) return;
      
      base.Interact();

      Debug.Log("Interaction");

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
