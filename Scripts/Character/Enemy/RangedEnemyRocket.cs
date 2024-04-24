using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyRocket : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private bool isAttack = true;
    [SerializeField] private float attackCoolDuration = 1;

    public LayerMask playerLayer;//play layer
    public GameObject bulletPrefab;
    protected Transform muzzlePos;
    private Transform player;
    protected Vector2 playerPos;
    protected Vector2 direction;
    protected float flipY;
    private SpriteRenderer sr;
    private Animator anim;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle");
        flipY = transform.localScale.y;
    }


    protected virtual void Update()
    {
        GetPlayerTransform();

        playerPos = player.position;
        //Debug.Log(playerPos);

        if (playerPos.x < transform.position.x)
            transform.localScale = new Vector3(flipY, -flipY, 1);
        else
            transform.localScale = new Vector3(flipY, flipY, 1);


        direction = (playerPos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;
        //Attack();
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
        anim.SetTrigger("fire");
        //Debug.Log("fire");
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = muzzlePos.position;
        float angel = Random.Range(-3f, 3f);
        bullet.GetComponent<Rocket>().SetSpeed(Quaternion.AngleAxis(angel, Vector3.forward) * direction);
        yield return new WaitForSeconds(attackCoolDuration);
        isAttack = true;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void GetPlayerTransform()
    {
        Collider2D[] clds = Physics2D.OverlapCircleAll(transform.position, 50, playerLayer);
        player = clds[0].transform;
    }

}
