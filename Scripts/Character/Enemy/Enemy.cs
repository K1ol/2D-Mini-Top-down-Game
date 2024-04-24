using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;
using UnityEditor;

public class Enemy : Character
{
    public UnityEvent<Vector2> OnMovementInput;

    public UnityEvent OnAttack;

    [SerializeField] private float chaseDistance = 1000f;
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float searchDistance = 1000f;

    private Transform player;
    private Seeker seeker;
    private List<Vector3> pathPointList;
    private int currentIndex = 0;
    private float pathGenerateInterval = 0.5f;

    private float pathGenerateTimer = 0f;
 

    [Header("attack")]
    public float damage;
    public LayerMask playerLayer;//play layer
    private bool isAttack = true;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    private void Start()
    {
        EnemyManager.Instance.enemyCount++;
        //Debug.Log("EnemyCount ++");
    }

    private void OnDestroy()
    {
        EnemyManager.Instance.enemyCount--;
    }

    private void Update()
    {
        GetPlayerTransform();

        if (GameManager.instance.currentGameState != GameState.inGame)
        {
            OnMovementInput?.Invoke(Vector2.zero);
            return;
        }


        if (player == null)
        {
            return;
        }

        float distance = Vector2.Distance(player.position, transform.position);

        if(distance < chaseDistance)//if the distance of enemy smaller than chaseDistance
        {
            AutoPath();
            if (pathPointList == null)
                return;

            if (distance <= (attackDistance/2))
            {
                //attack player
                OnMovementInput?.Invoke(Vector2.zero);//stop moving
                OnAttack?.Invoke();
            }
            else
            {
                //chase player

                //Vector2 direction = player.position - transform.position;
                Vector2 direction = (pathPointList[currentIndex] - transform.position).normalized;
                OnMovementInput?.Invoke(direction);
            }
        }
        else
        {
            //do not chase
            OnMovementInput?.Invoke(Vector2.zero);
        }

    }


    private void AutoPath()
    {
        pathGenerateTimer += Time.deltaTime;

        //generater path every fixed time
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            GeneratePath(player.position);
            pathGenerateTimer = 0;//reset timer
        }
        if(pathPointList == null || pathPointList.Count <= 0)
        {
            GeneratePath(player.position);
        } else if (Vector2.Distance(transform.position, pathPointList[currentIndex]) <= 0.1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count)
                GeneratePath(player.position);
        }
    }


    private void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        seeker.StartPath(transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
    }

    private void MeleeAttackAnimEvent() {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(attackDistance, attackDistance), 0f, playerLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Character>().TakeDamage(damage);
        }

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("hurt hero");
            //collision.GetComponent<Character>().TakeDamage(damage);

        } 
    }

    private void GetPlayerTransform()
    {
        Collider2D[] clds = Physics2D.OverlapCircleAll(transform.position, searchDistance, playerLayer);
        player = clds[0].transform;
    }

   

}
