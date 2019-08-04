using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Block" || collision.transform.tag == "Enemy"){
            Destroy(gameObject);
        }
    }
}
