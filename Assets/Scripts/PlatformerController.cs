using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : PhysicsObject
{

    
    public float MaxSpeedNormal = 7;
    public float TakeOffSpeedNormal = 10;



    public bool isModifyGravity = true;



    private float dashSpeed;
    public float dashSpeedStart;
    private float dashTime;
    public float dashStartTime;
    

    private float nextDashTime;
    public float DashRate;


    public float specialYForce;

    public float specialXForce;


    int facing = 1;

    public int isDash = 0;


    public GameObject dashTrail;

    public Animator anim;

    public bool isFalling = true;


    private void Awake()
    {
        dashTime = dashStartTime;
        //dashTrail.SetActive(false);
    }

    protected override void ComputeVelocity()
    {
        Vector2 horInput = Vector2.zero;

        horInput.x = Input.GetAxis("Horizontal");

        //flip
        if (horInput.x < 0)
        {
            facing = -1;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (horInput.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facing = 1;
        }

        if (Mathf.Abs(velocity.x) > 0)
        {
            //anim.SetBool("isRun", true);

        }
        else if (Mathf.Abs(horInput.x) == 0)
        {


            //anim.SetBool("isRun", false);
        }


        if (velocity.y <= 0)
        {
            //anim.SetBool("isJump", false);

        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = TakeOffSpeedNormal;
            isFalling = true;
            //anim.SetBool("isJump", true);
            
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y *= 0.5f;
            }

        }

        

        if (isDash == 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (Time.time >= nextDashTime)
                {
                    isDash = 1;
                    //dashTrail.SetActive(true);
                    //anim.SetTrigger("isDash");
                    nextDashTime = Time.time + DashRate;
                }


            }

        }
        else
        {
            if (dashTime <= 0)
            {
                isDash = 0;
                //dashTrail.SetActive(false);
                dashTime = dashStartTime;
                dashSpeed = 0;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if (isDash == 1)
                {
                    dashSpeed = dashSpeedStart;
                }
            }
        }


        velocity.y += specialYForce;

        Vector2 tspeed = horInput * MaxSpeedNormal;

        targetVelocity = new Vector2(tspeed.x + specialXForce + (facing * dashSpeed), tspeed.y);



    }





}

