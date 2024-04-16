using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbody;
    
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
        else
            rigidbody.MovePosition(rigidbody.position + Vector3.forward * animator.deltaPosition.magnitude);
    }
}
