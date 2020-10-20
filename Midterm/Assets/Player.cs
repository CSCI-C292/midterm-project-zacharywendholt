using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] Rigidbody2D rb;
    private bool _grounded = false;
    private float _jumpSpeed = 40;

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
    }

    void OnCollisionEnter2D(){
        _grounded = true;
    }

    void OnCollisionExit2D(){
        _grounded = false;
    }

    void move(){
        float horizontalMovement = Input.GetAxis ("Horizontal");
        Vector2 movement = new Vector2 (horizontalMovement * _moveSpeed, 0);
        transform.Translate(movement * Time.deltaTime);
    }

    void jump(){
        if (Input.GetKeyDown("space")&& _grounded == true){
            Vector2 movement = new Vector2 (0, _jumpSpeed * _moveSpeed);
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            rb.AddForce(movement);
            Debug.Log("jumped");
        }
    }
}
