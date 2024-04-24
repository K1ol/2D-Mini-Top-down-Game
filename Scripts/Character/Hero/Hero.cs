using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : Character
{
    public static Hero instance;

    void Awake()
    {
        instance = this;

    }
    public void MaxHealth()
    {
        currentHealth = maxHealth;
    }
    public float GetCurrentHP()
    {
        return currentHealth;
    }

}
