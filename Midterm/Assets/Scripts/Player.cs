using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    private bool _grounded = false;
    [SerializeField] private float _jumpSpeed = 40;
    [SerializeField] private int _maxMoveSpeed = 10;
    private Vector2 _speedReduction = new Vector2(.5f, 0);


    // Update is called once per frame

    void Start(){
        //DontDestroyOnLoad(gameObject);

        // if there is another camera in the scene delete it
        // this ensures that the player camera is the only active camera in the scene


    }
    void Update()
    {
        move();
        jump();

    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Ground"){
            _grounded = true;
        }

        if(collision.tag == "Obstacle"){
            rb.velocity = new Vector2(0,0);
            transform.position = new Vector2(0,0);
        }

        if(collision.tag == "Fly"){
            Score.IncreaseScore();
            Destroy(collision.gameObject);
        }
        
        if(collision.tag == "Enemy"){
            transform.position = new Vector2(0,0);
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Ground"){
            _grounded = false;
        }
    }

// current bug happens when the player is holding down the oppositie direction, and that direction is abo
    void move(){
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if(Math.Abs(rb.velocity.x) < _maxMoveSpeed){
                float targetHorizontalMovement = Input.GetAxis("Horizontal") * _maxMoveSpeed;
                Vector2 velocityChange = new Vector2(targetHorizontalMovement-rb.velocity.x * Time.deltaTime, 0);
                rb.AddForce(velocityChange);
        }


        }
        //player is moving in a negative direction
        if(rb.velocity.x < 0 && rb.velocity.x < -_maxMoveSpeed){
            rb.AddForce(_speedReduction);
        }
        else if(rb.velocity.x > 0 && rb.velocity.x > _maxMoveSpeed){
            rb.AddForce(-_speedReduction);
        }
 
    }


    

    void jump(){
        if (Input.GetKeyDown("space")&& _grounded == true){
            Vector2 movement = new Vector2 (0, _jumpSpeed);
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            rb.AddForce(movement, ForceMode2D.Impulse);

        }
    }

}
