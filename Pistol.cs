using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public static Pistol instance;


    [Header("Melee Attack")]
    public float meleeAttackDamage;
    [SerializeField] private float attackDistance = 3f;//attack size
    public LayerMask enemyLayer;
    public LayerMask obsLayer;

    private Vector2 AttackAreaPos;

    void Awake()
    {
        instance = this;
    }

    void MeleeAttackAnimEvent(int isAttack)
    {
        AttackAreaPos = transform.position;
        //Debug.Log("isAttack");
        Collider2D[] hitColliders1 = Physics2D.OverlapBoxAll(AttackAreaPos, new Vector2(attackDistance, attackDistance), 0f,enemyLayer);
        Collider2D[] hitColliders2 = Physics2D.OverlapBoxAll(AttackAreaPos, new Vector2(attackDistance, attackDistance), 0f,obsLayer);
        //Debug.Log(AttackAreaPos);
        //Debug.Log(attackDistance);
        foreach (Collider2D hitCollider in hitColliders1)
        {
            //Debug.Log("hitCollider");
            hitCollider.GetComponent<Character>().TakeDamage(meleeAttackDamage);
        }
        foreach (Collider2D hitCollider in hitColliders2)
        {
            //Debug.Log("hitCollider");
            hitCollider.GetComponent<destructibleObject>().Hit(2);
        }

    }

    //draw the Melee range on Unity
    //private void OnDrawGizmosSelected()
    //{
       // AttackAreaPos = transform.position;


       // Handles.color = Color.yellow;


       // Handles.DrawWireCube(AttackAreaPos, new Vector3(attackDistance, attackDistance, 0.1f));

   // }

}
