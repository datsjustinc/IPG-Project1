using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    private enum MovingPattern { EaseIn, EaseOut, Jump, Idle, Die }
    [SerializeField] private MovingPattern state;

    private Vector3 startPosition;
    private Vector3 currentTarget;

    private float movementTimer = 0;

    private void Start()
    {
        currentTarget = Vector3.up * 1f;
    }

    private void Update()
    {
        Move();
        
        Vector3 targetDistance = currentTarget - transform.position;
        targetDistance.y = 0;
        Vector3 moveDirection = targetDistance.normalized;
        Vector3 newPosition = Vector3.zero;

        if (state == MovingPattern.EaseIn)
        {
            movementTimer = Mathf.Min(1, movementTimer + Time.deltaTime);
            newPosition = startPosition + (currentTarget - startPosition) * (1 - Mathf.Cos((movementTimer * Mathf.PI) / 2));
        }

        else if (state == MovingPattern.EaseOut)
        {
            
        }

        else if (state == MovingPattern.Jump)
        {
           
        }

        else if (state == MovingPattern.Idle)
        {
            
        }

        else if (state == MovingPattern.Die)
        {
            
        }

        transform.position = newPosition;
        
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up * 1f);
        }
    }

    private void Move()
    {
        startPosition = transform.position;
        currentTarget = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        movementTimer = 0;
    }
}
