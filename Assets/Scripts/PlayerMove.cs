using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float movespeed;
    public float jumpforce;

        
    private Rigidbody2D playerRB;

    private bool isFacingRight = true;
    private float moveDirection;
    private bool isjumping = false;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //input control
        Inputs();

        //change facing direction
        ChangeDirection();


    }

    private void FixedUpdate()
    {
        //todo move
        Move();
    }

    private void Inputs()
    {
        moveDirection = Input.GetAxis("Horizontal");//-1~1
        if (Input.GetButtonDown("Jump"))
        {
            isjumping = true;
        }
    }

    private void Move()
    {
        playerRB.velocity = new Vector2(moveDirection * movespeed, playerRB.velocity.y);
        if (isjumping)
        {
            playerRB.AddForce(new Vector2(0, jumpforce));
            isjumping = false;
        }
    }

    private void ChangeDirection()
    {
        if (moveDirection > 0 && !isFacingRight)
        {
            FlipPlayer();
        }
        else if (moveDirection < 0 && isFacingRight)
        {
            FlipPlayer();
        }
    }


    private void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180f, 0);
    }



}
