using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;
    public Vector3 startingPosition;
    public GameObject gun;
    public float speed;
    private Vector2 input;
    private Vector2 mousePos;
    private Animator animator;
    private Rigidbody2D rb;

    public Transform GetTransform()
    {
        return transform;
    }

    void Awake()
    {
        instance = this;
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
        
        

    public void StartGame()
    {
        
        transform.position = startingPosition;
    }

    void FixedUpdate()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            
            rb.velocity = input.normalized * speed;
            if (mousePos.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            if (input != Vector2.zero)
                animator.SetBool("isMoving", true);
            else
                animator.SetBool("isMoving", false);

        } else
        {
            rb.velocity = input.normalized * 0;
            animator.SetBool("isMoving", false);
        }
           
           
    }

        public void HeroHurt()
        {
            animator.SetTrigger("hurt");
        }

       
}
