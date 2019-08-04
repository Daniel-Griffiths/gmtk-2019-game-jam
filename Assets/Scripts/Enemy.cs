using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private int health = 4;
    private float speed = 4f;
    private bool isMoving = false;
    private Direction enemyDirection = Direction.right;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int randomDirection = Random.Range(0, 2);

        // Pick a random direction on spawn
        if(randomDirection == 0){
            enemyDirection = Direction.left;
        } else {
            speed = speed * -1;
            enemyDirection = Direction.right;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(health == 0){
            FindObjectOfType<GameManager>().IncreaseScore();
            Destroy(gameObject);
        }

        if(isMoving){
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    void Update(){
        Vector2 enemyScale = transform.localScale;

        if(speed < 0){
            enemyScale.x = -1;
            enemyDirection = Direction.left;
        }

        if(speed > 0){
            enemyScale.x = 1;
            enemyDirection = Direction.right;
        }

        transform.localScale = enemyScale;

        // Check if the enemy fell into the hole
        if(transform.position.y < -5f){
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isMoving = true;

        if(collision.transform.tag == "Block"){
            speed = speed * -1;
        }

        if(collision.transform.tag == "Bullet"){
            health--;
        }
    }
}
