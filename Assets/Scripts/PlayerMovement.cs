using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbody;
    private int next_x_pos;
    private bool Left, Right;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Run", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Slide", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("Jump", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            if (!animator.GetBool("Jump") && !animator.GetBool("Slide"))
                animator.SetBool("Right", true);
            else 
                Right = true;
                
            if (rigidbody.position.x >= -3 && rigidbody.position.x < -1)
            {
                next_x_pos = 0;
            }
            else if (rigidbody.position.x >= -1 && rigidbody.position.x < 1)
            {
                next_x_pos = 2;
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Left", true);
            
            if (rigidbody.position.x >= 1 && rigidbody.position.x < 3)
            {
                next_x_pos = 0;
            }
            else if (rigidbody.position.x >= -1 && rigidbody.position.x < 1)
            {
                next_x_pos = -2;
            }
        }
    }

    
    //Animation events
    void ToggleOff(string Name)
    {
        animator.SetBool(Name, false);
        isJumpDown = false;
    }

    private bool isJumpDown = false;

    void JumpDown()
    { 
        isJumpDown = true;
    }
    
    private void OnAnimatorMove()
    {
        if (animator.GetBool("Jump"))
        {
            if (isJumpDown)
                rigidbody.MovePosition(rigidbody.position + new Vector3(0,0,2) * animator.deltaPosition.magnitude);
            else 
                rigidbody.MovePosition(rigidbody.position + new Vector3(0,1.5f,2) * animator.deltaPosition.magnitude);
        }
        else if (animator.GetBool("Right"))
        {
            if (rigidbody.position.x < next_x_pos) 
                rigidbody.MovePosition(rigidbody.position + new Vector3(1,0,1.5f) * animator.deltaPosition.magnitude);
            else
                animator.SetBool("Right",false);
        }
        else if (animator.GetBool("Left"))
        {
            if (rigidbody.position.x > next_x_pos) 
                rigidbody.MovePosition(rigidbody.position + new Vector3(-1,0,1.5f) * animator.deltaPosition.magnitude);
            else
                animator.SetBool("Left",false);
        }
        else
            rigidbody.MovePosition(rigidbody.position + Vector3.forward * animator.deltaPosition.magnitude);
    }
}
