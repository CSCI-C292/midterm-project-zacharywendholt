using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 movementVector = new Vector2();
    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = .3f;
        StartCoroutine("ChangeDirection");

    }

    // Update is called once per frame
    void Update()
    {
        movementVector = new Vector2(moveSpeed * Time.deltaTime, 0);
        transform.Translate(movementVector);
    }

    IEnumerator ChangeDirection()
    {
        moveSpeed = moveSpeed *-1;
        yield return new WaitForSecondsRealtime(3.0f);
        StartCoroutine("ChangeDirection");
    }

        void OnTriggerEnter2D(Collider2D collision){
            if(collision.name == "KnifeHitbox"){

                Destroy(gameObject);            
            }

            else if(collision.name == "Player"){
                collision.transform.position = new Vector2(0,0);
                Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
                Vector2 antiVelocity = new Vector2(-playerRigidbody.velocity.x, -playerRigidbody.velocity.y);
                playerRigidbody.AddForce(antiVelocity,ForceMode2D.Impulse);
            }



    }
}
