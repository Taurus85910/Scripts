using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private bool isEnemy;
    [SerializeField] private int damage;
    [SerializeField] private PlayerMoney playerMoney;
    private void OnTriggerEnter(Collider other)
    {
        
        if (isEnemy)
        {
            print("Hit");
            if (other.TryGetComponent<PlayerHealth>(out var playerHealth))
            {
                print("Found");
                playerHealth.TakeDamage(damage);
            }
        }
        else
        {
            if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                playerMoney.AddMoney(enemyHealth.TakeDamage(damage));
            }
        }
    }

}
