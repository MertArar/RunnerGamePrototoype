using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1.5f;
    [SerializeField] private float jumpingSpeed = 1.7f;
    [SerializeField] private float slidingSpeed = 1.7f;
    [SerializeField] private GameObject player;
    
    
    private Animator animator;
    private Rigidbody rb;
    private int next_x_pos;
    private bool Left, Right;
    public static int currentTile = 0;
    private float speedIncreaseInterval = 15f; 
    private float speedIncreaseAmount = 0.2f; 
    private float maxSpeed = 10f;
    
    private bool canMove = true;
    static public bool currentlyMove = false;
    private bool isJumpDown = false;
    private bool isSlidingUp = false;
    
    public AudioSource slideFX;
    public AudioSource jumpFX;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        StartCoroutine(IncreaseSpeedRoutine());
    }

    void Update()
    {
        animator.SetBool("Run", true);
        if (currentlyMove == true)
        {
            
            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("Slide", true);
                slideFX.Play();
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("Jump", true);
                jumpFX.Play();
            }
            else if (Input.GetKeyUp(KeyCode.D) && canMove == true)
            {
                if (!animator.GetBool("Jump") && !animator.GetBool("Slide"))
                    animator.SetBool("Right", true);
                else
                    Right = true;

                if (rb.position.x >= -3 && rb.position.x < -1)
                {
                    next_x_pos = 0;
                }
                else if (rb.position.x >= -1 && rb.position.x < 1)
                {
                    next_x_pos = 2;
                }

                StartCoroutine(ToRight(next_x_pos));

            }
            else if (Input.GetKeyUp(KeyCode.A) && canMove == true)
            {
                if (!animator.GetBool("Jump") && !animator.GetBool("Slide"))
                    animator.SetBool("Left", true);
                else
                    Left = true;
                if (rb.position.x >= 1 && rb.position.x < 3)
                {
                    next_x_pos = 0;
                }
                else if (rb.position.x >= -1 && rb.position.x < 1)
                {
                    next_x_pos = -2;
                
                }

            
                StartCoroutine(ToLeft(next_x_pos));
            
            }
        }
        
         
    }
    IEnumerator ToLeft(int next_x_pos)
    {
        canMove = false;

        float timer = 0.0125f;
        yield return new WaitForSeconds(timer);
        transform.position = new Vector3(this.next_x_pos + 0.8f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos + 0.6f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos + 0.4f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos + 0.2f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos, transform.position.y, transform.position.z + (playerSpeed/625)*100);

        canMove = true;
    }
    IEnumerator ToRight(int next_x_pos)
    {
        canMove = false;

        float timer = 0.0125f;
        yield return new WaitForSeconds(timer);
        transform.position = new Vector3(this.next_x_pos - 0.8f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos - 0.6f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos - 0.4f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos - 0.2f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos, transform.position.y, transform.position.z + (playerSpeed / 625)*100);

        canMove = true;
    }
    //Animation events
    void ToggleOff(string Name)
    {
        animator.SetBool(Name, false);
        isJumpDown = false;
    }
    
    void JumpDown()
    {
        isJumpDown = true;
    }

    void SlideUp()
    {
        isSlidingUp = true;
    }

    private void OnAnimatorMove()
    {
        if (animator.GetBool("Jump"))
        {
            if (isJumpDown)
                rb.MovePosition(rb.position + new Vector3(0, 0, 2) * animator.deltaPosition.magnitude * jumpingSpeed);
            else
                rb.MovePosition(rb.position + new Vector3(0, 1.5f, 2) * animator.deltaPosition.magnitude * jumpingSpeed);
        }
        
        else if (animator.GetBool("Slide"))
        {
            if (isSlidingUp)
                rb.MovePosition(rb.position + new Vector3(0,0,2) * animator.deltaPosition.magnitude * slidingSpeed);
            else
                rb.MovePosition(rb.position + new Vector3(0,0,2) * animator.deltaPosition.magnitude * slidingSpeed);
        }
        
        else if (animator.GetBool("Right"))
        {
            if (rb.position.x < next_x_pos)
                rb.MovePosition(rb.position + new Vector3(1, 0, 1.5f) * animator.deltaPosition.magnitude);
            else
                animator.SetBool("Right", false);
        }
        else if (animator.GetBool("Left"))
        {
            if (rb.position.x > next_x_pos)
                rb.MovePosition(rb.position + new Vector3(-1, 0, 1.5f) * animator.deltaPosition.magnitude);
            else
                animator.SetBool("Left", false);
        }
        else
        {
            float currentSpeed = Mathf.Min(playerSpeed, maxSpeed);
            rb.MovePosition(rb.position + Vector3.forward * animator.deltaPosition.magnitude * currentSpeed);
        }

        if (Left)
        {
            if (rb.position.x > next_x_pos)
                rb.MovePosition(rb.position + new Vector3(-1, 0, 0) * animator.deltaPosition.magnitude);
            else
                Left = false;
        }

        else if (Right)
        {
            if (rb.position.x < next_x_pos)
                rb.MovePosition(rb.position + new Vector3(1, 0, 0) * animator.deltaPosition.magnitude);
            else
                Right = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obs"))
        {
            animator.SetBool("Dead", true);
        }
    }

    IEnumerator IncreaseSpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            playerSpeed += speedIncreaseAmount;
        }
    }
}
