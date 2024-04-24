using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;
using UnityEditor;

public class RangedEnemy : Character
{
    public UnityEvent<Vector2> ROnMovementInput;

    public UnityEvent ROnAttack;
    private RaycastHit2D hit;

    [SerializeField] private float chaseDistance = 100f;
    [SerializeField] private float safeDistance = 5f;


    private Transform player;
    private Seeker seeker;
    private List<Vector3> pathPointList;
    private int currentIndex = 0;
    private float pathGenerateInterval = 0.5f;
    private float pathGenerateTimer = 0f;

    [Header("attack")]
    public float damage;
    public LayerMask playerLayer;//play layer
    public float Cooldown = 2f;//cooldown
    private bool isAttack = true;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        player = PlayerMovement.instance.GetTransform();
    }

    private void Start()
    {
        EnemyManager.Instance.enemyCount++;
        Debug.Log("RangedEnemyCount ++");
    }

    private void OnDestroy()
    {
        EnemyManager.Instance.enemyCount--;
    }

    private void Update()
    {

        if (GameManager.instance.currentGameState != GameState.inGame)
        {
            ROnMovementInput?.Invoke(Vector2.zero);
            return;
        }
            


        if (player == null)
        {
            return;
        }

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance < chaseDistance)//if the distance of enemy smaller than chaseDistance
        {
            AutoPath();
            // ��������Ƿ��赲
            Vector2 rayDirection = (player.position - transform.position).normalized;
            if (Physics2D.Raycast(transform.position, rayDirection,30, LayerMask.GetMask("obstacles")))
            {
                // ������������ϰ�������
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("obstacles"))
                {
                    // ������
                }
                else
                {
                    // ����δ���ϰ����ڵ������Կ���
                    ROnAttack?.Invoke();
                }
            }
            else
            {
                // ����δ���κ������ڵ������Կ���
                ROnAttack?.Invoke();
            }
            

            if (pathPointList == null)
                return;

            if (distance <= safeDistance)
            {

                Vector2 direction = (transform.position - player.position).normalized;
                ROnMovementInput?.Invoke(direction);//
            }
            else
            {
                //chase player

                //Vector2 direction = player.position - transform.position;
                Vector2 direction = (pathPointList[currentIndex] - transform.position).normalized;
                ROnMovementInput?.Invoke(direction);
            }
        }
        else
        {
            //do not chase
            ROnMovementInput?.Invoke(Vector2.zero);
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
        if (pathPointList == null || pathPointList.Count <= 0)
        {
            GeneratePath(player.position);
        }
        else if (Vector2.Distance(transform.position, pathPointList[currentIndex]) <= 0.1f)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("hurt hero");
            //collision.GetComponent<Character>().TakeDamage(damage);

        }
    }

 



}
