using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float interval1;
    public float interval2;
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    protected Transform muzzlePos;
    protected Transform shellPos;
    protected Vector2 mousePos;
    protected Vector2 direction;
    protected float timer1;
    protected float timer2;
    protected float flipY;
    protected Animator animator;
    



    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle");
        shellPos = transform.Find("BulletShell");
        flipY = transform.localScale.y;
    }

    protected virtual void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (GameManager.instance.currentGameState == GameState.inGame)
            Shoot();
    }

    protected virtual void Shoot()
    {

        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;

        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3(flipY, -flipY, 1);
        else
            transform.localScale = new Vector3(flipY, flipY, 1);

        if (timer1 != 0)
        {
            timer1 -= Time.deltaTime;
            if (timer1 <= 0)
                timer1 = 0;
        }
        if (timer2 != 0)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
                timer2 = 0;
        }

        if (Input.GetButton("Fire1"))
        {
            if (timer1 == 0)
            {
                timer1 = interval1;
                Fire();
            }
        }
        if (Input.GetButton("Fire2"))
        {
            if (timer2 == 0)
            {
                timer2 = interval2;
                Atk();
            }
        }

    }

    protected virtual void Atk()
    {
        animator.SetTrigger("MeleeAtk");
    }

    protected virtual void Fire()
    {
        SoundManager.PlayShootClip();
        animator.SetTrigger("Shoot");

        // GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = muzzlePos.position;

        float angel = Random.Range(-3f, 3f);
        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angel, Vector3.forward) * direction);

        // Instantiate(shellPrefab, shellPos.position, shellPos.rotation);
        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }

    public float GetTimer1()
    {
        return timer1;
    } 
    public float GetTimer2()
    {
        return timer2;
    }


}
