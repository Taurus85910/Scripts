using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float attackDuration = 1f;
    [SerializeField] private GameObject weaponStorage;
    [SerializeField] private float attackCooldown = 2f;
    
    
    
    private GameObject currentWeapon;
    private Collider currentCollider;
    private float lastAttackTime = 0f;
    private void Start()
    { 
       
        animator = GetComponent<Animator>();
        UpdateWeapon();
        currentCollider.enabled = false;
    }

    public void UpdateWeapon()
    {
        foreach (Transform weapon in weaponStorage.transform)
        {
            if (weapon.gameObject.activeSelf)
            {
                currentWeapon = weapon.gameObject;
                currentCollider = currentWeapon.GetComponent<Collider>();
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time - lastAttackTime < attackCooldown)
                return;
            StartCoroutine(PerformAttack());
            lastAttackTime = Time.time;
        }
    }

    private IEnumerator PerformAttack()
    {
        currentCollider.enabled = true;

        animator.SetBool("IsAttacking", true);
        
        yield return new WaitForSeconds(0.1f);
        
        animator.SetBool("IsAttacking", false);
        
        yield return new WaitForSeconds(attackDuration);
        
        currentCollider.enabled = false;
    }
}
