using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject _knifeHitbox;
    [SerializeField] GameObject _obstaclePrefab;
    private bool _grounded = false;
    [SerializeField] private float _jumpSpeed = 40;
    [SerializeField] private int _maxMoveSpeed = 10;
    private Vector2 _speedReduction = new Vector2(.5f, 0);

    private Vector3 grappleLocation;
    private LineRenderer lineRenderer;


    // Update is called once per frame

    void Start(){
        initializeLineRenderer();

        Physics2D.IgnoreLayerCollision(9,9,true);
    }
    void Update()
    {
        move();
        jump();
        attack();
        grapple();

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

    void attack(){
        if(Input.GetMouseButton(0)){
            _knifeHitbox.SetActive(true);
        }
        else{
            _knifeHitbox.SetActive(false);
        }
    }

    void grapple(){

        

        if(Input.GetMouseButtonDown(1)){
            grappleLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, grappleLocation);
        }
        else if(Input.GetMouseButtonUp(1)){
            grappleLocation = transform.position;
        }

        else if(Input.GetMouseButton(1)){
            Vector3 grappleForce = grappleLocation - transform.position;
            rb.AddForce(new Vector2(grappleForce.x, grappleForce.y * 3));
            lineRenderer.SetPosition(1, transform.position);
        }
        else{
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }
    void initializeLineRenderer(){
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        //lineRenderer.startColor = Color.black;
        //lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
        lineRenderer.material = whiteDiffuseMat;
    }

}
