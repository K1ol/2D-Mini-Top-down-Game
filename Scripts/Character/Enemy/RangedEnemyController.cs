using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangedEnemyController : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private float currentSpeed = 0;
    public UnityEvent Dead;//destroy weapon
    public Vector2 MovementInput { get; set; }

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Collider collider;

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
        }
        else if (GameManager.instance.currentGameState == GameState.gameOver)
        {
            isDead = true;
        }

        SetAnimation();
    }

    void Move()
    {
        if (MovementInput.magnitude > 0.1f && currentSpeed >= 0)
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
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

  

    public void EnemyHurt()
    {
        anim.SetTrigger("hurt");
    }

    public void EnemyDead()
    {
        AddScore();

        isDead = true;
        //collider.enabled = false;
        rb.velocity = Vector2.zero;
    }


    void SetAnimation()
    {
        anim.SetBool("isWalk", MovementInput.magnitude > 0);
        
        anim.SetBool("isDead", isDead);

    }

    void AddScore()
    {
        if (GameManager.instance.currentGameState != GameState.gameOver)
        {
            GameManager.instance.score += 20;
        }
    }


    public void DestroyREnemy()
    {
        //Debug.Log("Enemy Dead");
        
        Destroy(this.gameObject);
    }

    public void DestroyWeapon()
    {
        Dead?.Invoke();//destroy weapon
        DestroyREnemy();
    }

}
