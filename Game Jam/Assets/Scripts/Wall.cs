using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startingPos = Vector3.zero;
    public Vector3 targetPosition = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private float moveAmount = 2.5f;
    public float moveSpeed = 5f;
    public float amountMoved = 0;
    private bool move = false;
    void Start()
    {
        startingPos = transform.position;
        moveAmount = Vector3.Distance(targetPosition, startingPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            moveDirection = targetPosition - transform.position;
            moveDirection.y = 0;
            moveDirection = moveDirection.normalized;
            Vector3 movementAmount = moveDirection * (moveSpeed * Time.deltaTime);
            amountMoved += movementAmount.magnitude;
            transform.Translate(movementAmount);
            if (amountMoved >= moveAmount)
            {
                move = false;
                amountMoved = 0;

                moveAmount = Vector3.Distance(targetPosition, startingPos);
                transform.position = targetPosition;
                Vector3 buffer = targetPosition;
                targetPosition = startingPos;
                startingPos = buffer;
            }
        }
    }
    public void StartMove()
    {
        move = true;
    }
    public void SetMoveData(float a_moveSpeed, Vector3 a_targetPosition)
    {
        moveSpeed = a_moveSpeed;
        //set this so the target is always the same height so it doesnt move
        targetPosition = a_targetPosition;
        targetPosition.y = transform.position.y;
        moveDirection = targetPosition - transform.position;
        moveDirection.y = 0;
        moveDirection = moveDirection.normalized;
    }
}
