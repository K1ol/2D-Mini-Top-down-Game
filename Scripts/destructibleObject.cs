using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleObject : MonoBehaviour
{
    public int hp=3;
    // Start is called before the first frame update


  void Update()
    {
        if(hp<=0)
            Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        hp -= damage;
        //Debug.Log("obstacle hp - 1 ");
    }
}
