using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D playerRB;
    public float movespeed;
    public float jumpforce;
    
    public Transform groundcheck;
    public Transform ceilingcheck;
    public LayerMask groundobjects;
    public Vector2 checkboxsize = new Vector2(0.5f, 0.1f);
    public int maxjumpcount = 2;

    private bool isFacingRight = true;
    private float moveDirection;
    private bool rdyjump = false;
    private bool onjumping = false;
    private bool isGrounded=false;
    private int jumpcount=2;

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
        JumpingCheck();

        BoarderCheck();

        //todo move
        Move();
    }

    private void Inputs()
    {
        moveDirection = Input.GetAxis("Horizontal");//-1~1
        if (Input.GetButtonDown("Jump") && jumpcount<2)
        {
            rdyjump = true;
        }
    }

    private void Move()
    {
        playerRB.velocity = new Vector2(moveDirection * movespeed, playerRB.velocity.y);
        if (rdyjump)
        {
            playerRB.AddForce(new Vector2(0, jumpforce));
            jumpcount++;
            rdyjump = false;
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

    private void BoarderCheck()
    {
        isGrounded = Physics2D.OverlapBox(groundcheck.position, checkboxsize ,groundobjects);
        if (isGrounded&& !onjumping)//检测位置完全处于本地下方才能生效，起跳的时候容易还是检测到贴合地面导致连续重置，增加判定是否已经处于Y轴移动中，规避连跳上限++
        {
            jumpcount = 0;
        }
    }

    private void JumpingCheck()
    {
        if (playerRB.velocity.y != 0)
        {
            onjumping = true;
        }
        else
        {
            onjumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundcheck.position , new Vector3(checkboxsize.x, checkboxsize.y)*2);
    }

}
