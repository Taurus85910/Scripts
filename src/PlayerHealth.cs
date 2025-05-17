using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private int maxHealth = 150;
    [SerializeField] private GameObject deathPanel;
    
    private int health;
    
    private void Start()
    {
        health = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = ((float)health/maxHealth) * 100;
        if (health <= 0)
        { 
            Time.timeScale = 0f;
            health = maxHealth;
            deathPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            healthSlider.value = healthSlider.maxValue;
        }
    }
}
