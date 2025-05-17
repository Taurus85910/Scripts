using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 50;
    [SerializeField] GameObject parent;
    [SerializeField] private Slider healthSlider;
    
    [SerializeField] private int lowReward;
    [SerializeField] private int highReward;
    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public int TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = ((float)currentHealth/maxHealth) * 100;
        if (currentHealth <= 0)
        {
            healthSlider.value = 100;
            currentHealth = maxHealth;
            parent.SetActive(false);
            return Random.Range(lowReward, highReward);
        }
        return 0;
    }
}
