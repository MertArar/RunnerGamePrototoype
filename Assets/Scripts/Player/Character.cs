using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum Side {Left, Mid, Right}
public class Character : MonoBehaviour
{
    public Side mSide = Side.Mid;
    private float newXPos = 0f;
    public bool swipeLeft;
    public bool swipeRight;
    public float xValue;
    private CharacterController player;
    void Start()
    {
        player = GetComponent<CharacterController>();
        transform.position = Vector3.zero;
    }

    void Update()
    {
        swipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        swipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);

        if (swipeLeft)
        {
            if (mSide == Side.Mid)
            {
                newXPos = -xValue;
                mSide = Side.Left;
            }
            else if (mSide == Side.Right)
            {
                newXPos = 0;
                mSide = Side.Mid;
            }
        }

        else if (swipeRight)
        {
            if (mSide == Side.Mid)
            {
                newXPos = xValue;
                mSide = Side.Right;
            }
            else if (mSide == Side.Left)
            {
                newXPos = 0;
                mSide = Side.Mid;
            }
        }

        player.Move((newXPos - transform.position.x) * Vector3.right);
    }
}
