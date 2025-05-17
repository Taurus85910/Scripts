using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] private GameObject attackArea;
    [SerializeField] private float attackDuration = 1f;

    private Collider collider;
    private Animator animator;
    private void Start()
    {
        collider = attackArea.GetComponent<Collider>();
        collider.enabled = false;
        animator = GetComponent<Animator>();
    }

    public IEnumerator PerformAttack()
    {
        collider.enabled = true;

        animator.SetBool("IsAttacking", true);
        
        yield return new WaitForSeconds(0.1f);
        
        animator.SetBool("IsAttacking", false);
        
        yield return new WaitForSeconds(attackDuration);
        
        collider.enabled = false;
    }
}
