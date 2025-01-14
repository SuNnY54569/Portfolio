using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
   private Enemy_Behaviour enemyParent;
   private bool inRange;
   private Animator anim;

   private void Awake()
   {
      enemyParent = GetComponentInParent<Enemy_Behaviour>();
      anim = GetComponentInParent<Animator>();
   }

   private void Update()
   {
      if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
      {
         enemyParent.Flip();
      }
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.CompareTag("Player"))
      {
         inRange = true;
      }
   }

   private void OnTriggerExitD(Collider2D col)
   {
      inRange = false;
      gameObject.SetActive(false);
      enemyParent.triggerArea.SetActive(true);
      enemyParent._inRange = false;
      enemyParent.SelectTarget();
   }
}
