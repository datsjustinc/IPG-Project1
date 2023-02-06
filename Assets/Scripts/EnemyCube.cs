using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    private enum MovingPattern { EaseIn, EaseOut, Jump, Idle, Die }
    [SerializeField] private MovingPattern state;
    
    private Vector3 currentTarget;
    
    // health bar status
    private float currentHealth;
    private float maxHealth = 100;

    private void Start()
    {
        currentHealth = maxHealth;
        currentTarget = Vector3.up * 1f;
    }

    private void Update()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up * 1f;
        
        Vector3 targetDistance = currentTarget - transform.position + Vector3.up * 1f;
        targetDistance.y = 0;
        Vector3 moveDirection = targetDistance.normalized;
        Vector3 newPosition = Vector3.zero;

        // easing towards target
        if (state == MovingPattern.EaseIn)
        {
            newPosition = Vector3.Lerp(transform.position, currentTarget + Vector3.back * 0.8f, 0.8f * Time.deltaTime);
        }
        
        // bouncing back from target
        else if (state == MovingPattern.EaseOut)
        {
            newPosition = Vector3.Lerp(transform.position, currentTarget + Vector3.back * 1.8f, 1.2f * Time.deltaTime);
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

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
