using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ZombieDamage : MonoBehaviour
{
    public int maxHealth = 100;
    public UnityEvent<ZombieDamage> OnDeath;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            OnDeath.Invoke(this);
        }
    }
}
