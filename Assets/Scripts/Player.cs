using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject gunEnd;
    public GameObject bullet;
    public AudioClip shootSound;
    public AudioClip deathSound;
    public AudioClip jumpSound;

    private float movementSpeed = 8f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private float horizontalVelocity = 0f;
    private bool isJumping = false;
    private Direction playerDirection = Direction.right;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate () {
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalVelocity = Input.GetAxisRaw("Horizontal") * movementSpeed;

        if(!isJumping && (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up"))){
            isJumping = true;
            rb.velocity += 10f * Vector2.up;
            audioSource.PlayOneShot(jumpSound);
        }

        if(Input.GetKeyDown("e") || Input.GetMouseButtonDown(0)){
            Shoot();
        }

        Vector2 playerScale = transform.localScale;

        if(Input.GetAxisRaw("Horizontal") < 0){
            playerScale.x = -1;
            playerDirection = Direction.left;
        }

        if(Input.GetAxisRaw("Horizontal") > 0){
            playerScale.x = 1;
            playerDirection = Direction.right;
        }

        transform.localScale = playerScale;
        
        // Check if the player fell into the hole
        if(transform.position.y < -5f){
             FindObjectOfType<GameManager>().GameOver();
        }
    }

    void Shoot(){

        float bulletDirection = playerDirection == Direction.left ? -600f : 600f;

        Vector2 bulletPosition = new Vector2(bulletDirection, Random.Range(-10f, 10f));

        Instantiate(bullet, gunEnd.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>()
            .AddForce(bulletPosition);

            audioSource.PlayOneShot(shootSound);

            CameraShake.Shake(.1f,.1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Block"){
            isJumping = false;
        }

        if(collision.transform.tag == "Enemy"){
            FindObjectOfType<GameManager>().GameOver();
            audioSource.PlayOneShot(deathSound);
            Destroy(gameObject);
        }
    }
}
