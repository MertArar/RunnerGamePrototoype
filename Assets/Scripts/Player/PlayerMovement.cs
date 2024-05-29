using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2f;
    [SerializeField] private float jumpingSpeed = 1.2f;
    [SerializeField] private float slidingSpeed = 1.1f;
    [SerializeField] public float swipeThreshold = 50f;
    [SerializeField] private TextMeshProUGUI CoinsText;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject camObject;

    
    private Animator animator;
    private Rigidbody rb;
    
    public static int currentTile = 0;
    private int next_x_pos;
    private float maxSpeed = 8f;
    private float speedIncreaseInterval = 15f; 
    private float speedIncreaseAmount = 0.2f; 
    private int CoinsCollected;
    public int DistanceCollected;
    
    static public bool currentlyMove = false;
    private bool Left, Right;
    private bool canMove = true;
    private bool isJumpDown = false;
    private bool isSlidingUp = false;
    private bool swipeStarted;

    private Vector2 startPoint;
    public AudioSource slideFX;
    public AudioSource jumpFX;
    public static CollectableControl collectableControl;
    public GameObject gameOver;
    public GameObject dustEffect;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        StartCoroutine(IncreaseSpeedRoutine());
    }

    void Update()
    {
        if (animator.GetBool("Dead") || animator.GetBool("FallDead"))
        {
            PlayerPrefs.SetInt("CoinsCollected", CoinsCollected);
            StartCoroutine(waitGameOver());
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            startScreen.SetActive(false);
            animator.SetBool("Run", true);
            dustEffect.SetActive(true);
            swipeStarted = false;
        }
        if (currentlyMove == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swipeStarted = true;
                startPoint = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swipeStarted = false;
                Vector2 endPoint = Input.mousePosition;
                float swipeDistance = (endPoint - startPoint).magnitude;

                if (swipeDistance >= swipeThreshold)
                {
                    Vector2 swipeDirection = endPoint - startPoint;
                    swipeDirection.Normalize();

                    if (swipeDirection.x < -0.5f && Mathf.Abs(swipeDirection.y) < 0.5 && canMove == true)
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
                    else if (swipeDirection.x > 0.5f && Mathf.Abs(swipeDirection.y) < 0.5f && canMove == true)
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
                    else if (swipeDirection.y > 0.5f && Mathf.Abs(swipeDirection.x) < 0.5f)
                    {
                        rb.position = new Vector3(next_x_pos, transform.position.y, transform.position.z);
                        animator.SetBool("Slide", false);
                        animator.SetBool("Left", false);
                        animator.SetBool("Right", false);
                        animator.SetBool("Jump", true);
                        jumpFX.Play();
                    }
                    else if (swipeDirection.y < -0.5f && Mathf.Abs(swipeDirection.x) < 0.5f)
                    {
                        animator.SetBool("Jump", false);
                        animator.SetBool("Slide", true);
                        slideFX.Play();
                    }
                }
            }
        }
    }

    IEnumerator waitGameOver()
    {
        float timer = 1.5f;
        yield return new WaitForSeconds(timer);
        gameOver.SetActive(true);
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
        if (animator.GetBool("FallDead"))
        {
            rb.MovePosition(rb.position + Vector3.down * animator.deltaPosition.magnitude);
        }
        
        else if (animator.GetBool("Jump"))
        {
            if (isJumpDown)
                rb.MovePosition(rb.position + new Vector3(0, 0, 2.5f) * animator.deltaPosition.magnitude * jumpingSpeed);
            else
                rb.MovePosition(rb.position + new Vector3(0, 1.5f, 2.5f) * animator.deltaPosition.magnitude * jumpingSpeed);
        }
        
        else if (animator.GetBool("Slide"))
        {
            if (isSlidingUp)
                rb.MovePosition(rb.position + new Vector3(0,0,2.2f) * animator.deltaPosition.magnitude * slidingSpeed);
            else
                rb.MovePosition(rb.position + new Vector3(0,0,2.2f) * animator.deltaPosition.magnitude * slidingSpeed);
        }
        
        else if (animator.GetBool("Right"))
        {
            if (rb.position.x < next_x_pos)
                rb.MovePosition(rb.position + new Vector3(1, 0, 1.5f) * animator.deltaPosition.magnitude);
            else
            {
                rb.position = new Vector3(next_x_pos, transform.position.y, transform.position.z);
                animator.SetBool("Right", false);
            }
                
        }
        
        else if (animator.GetBool("Left"))
        {
            if (rb.position.x > next_x_pos)
                rb.MovePosition(rb.position + new Vector3(-1, 0, 1.5f) * animator.deltaPosition.magnitude);
            else
            {
                rb.position = new Vector3(next_x_pos, transform.position.y, transform.position.z);
                animator.SetBool("Left", false);
            }
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
        if (collision.collider.CompareTag("FallDamage"))
        {
            animator.SetBool("FallDead", true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            CoinsCollected++;
            CoinsText.text = CoinsCollected.ToString();
        }

        if (other.CompareTag("FallDamage"))
        {
            camObject.transform.parent = null;
            animator.SetBool("FallDead", true);
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
 