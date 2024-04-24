using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private float currentSpeed = 0;

    public Vector2 MovementInput { get; set; }

    [Header("Attack")]
    [SerializeField] private bool isAttack = true;
    [SerializeField] private float attackCoolDuration = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public Collider collider;

    private bool isDead;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (!isDead && GameManager.instance.currentGameState != GameState.gameOver)
        {
            Move();
        } else if(GameManager.instance.currentGameState == GameState.gameOver)
        {
            isDead = true;
        }

        

        SetAnimation();
    }

    void Move()
    {
        if(MovementInput.magnitude>0.1f && currentSpeed >= 0)
        {
            rb.velocity = MovementInput * currentSpeed;
            //towards
            if (MovementInput.x < 0)
            {
                sr.flipX = false;
            }
            if (MovementInput.x > 0)
            {
                sr.flipX = true;
            }
        }
        else {
            rb.velocity = Vector2.zero;
        }
    }

    public void Attack()
    {
        if (isAttack)
        {
            isAttack = false;
            StartCoroutine(nameof(AttackCoroutine));
        }
    }

    IEnumerator AttackCoroutine()
    {
        anim.SetTrigger("atk");

        yield return new WaitForSeconds(attackCoolDuration);
        isAttack = true;
    }

    public void EnemyHurt()
    {
        anim.SetTrigger("hurt");
    }

    public void EnemyDead()
    {
        AddScore();
        
        isDead = true;
        collider.enabled = false;
        rb.velocity = Vector2.zero;

    }


    void SetAnimation()
    {
        anim.SetBool("Walk", MovementInput.magnitude > 0);

        anim.SetBool("isDead", isDead);

    }

    void AddScore()
    {
        if (GameManager.instance.currentGameState != GameState.gameOver)
        {
            GameManager.instance.score += 10;
        }
    }

    public void DestroyEnemy()
    {

        Destroy(this.gameObject);
    }


}
