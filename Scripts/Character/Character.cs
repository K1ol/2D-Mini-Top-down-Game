using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField]protected float maxHealth;

    [SerializeField]public float currentHealth;

    public UnityEvent OnHurt;
    public UnityEvent OnDead;




    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
    }


    public virtual void TakeDamage(float damage)
    {
        

        if(currentHealth - damage > 0f)
        {
            currentHealth -= damage;
            Debug.Log("CurrentHealth:" + currentHealth);
            OnHurt?.Invoke();
        }
        else
        {
            Die();
        }
    }


    public virtual void Die()
    {
        currentHealth = 0f;
        OnDead?.Invoke();
        Debug.Log("Invoke Dead");
        //Destroy(this.gameObject);
    }

}
