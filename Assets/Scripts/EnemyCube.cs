using System.Collections;
using StarterAssets;
using UnityEngine;

/// <summary>
/// This class manages enemy AI.
/// </summary>
public class EnemyCube : MonoBehaviour
{
    // enemy action states
    public enum MovingPattern { EaseIn, EaseOut, BackAway, Die }
    [SerializeField] public MovingPattern state;
    
    private Vector3 currentTarget;
    
    // health bar status
    [SerializeField] private Healthbar health;
    private float currentHealth;
    private float maxHealth = 100;

    private Renderer rend;

    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        currentHealth = maxHealth;
        currentTarget = Vector3.up * 0.7f;
        rend = gameObject.GetComponent<Renderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// This function runs every frame and helps manages enemy states.
    /// </summary>
    private void Update()
    {
        if (currentHealth <= 0)
        {
            state = MovingPattern.Die;
        }

        currentTarget = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up * 0.7f;

        Vector3 targetDistance = currentTarget - transform.position + Vector3.up * 0.7f;
        targetDistance.y = 0;
        Vector3 moveDirection = targetDistance.normalized;
        Vector3 newPosition = Vector3.zero;

        // easing towards target
        if (state == MovingPattern.EaseIn)
        {
            rend.material.color = Color.white;

            print("EaseIn");
            newPosition = Vector3.Lerp(transform.position, currentTarget + Vector3.back * 0.5f, 0.5f * Time.deltaTime);
        }

        // bouncing back from target
        else if (state == MovingPattern.EaseOut)
        {
            print("EaseOut");
            rend.material.color = Color.red;
            newPosition = Vector3.Lerp(transform.position, currentTarget + Vector3.back * 7.8f, 1.8f * Time.deltaTime);
        }
        
        // reset for another attack
        else if (state == MovingPattern.BackAway)
        {
            print("BackAway");
            newPosition = Vector3.Lerp(transform.position, currentTarget + Vector3.back * 4f, 1f * Time.deltaTime);
        }

        // Death animation
        else if (state == MovingPattern.Die)
        {
            rend.material.color = Color.red;
            // enlarged
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 2, Time.deltaTime * 7);

            // destroyed when at biggest
            if (transform.localScale == Vector3.one * 2)
            {
                // remove enemy object from game manager list
                gameManager.GetEnemies().Remove(gameObject);
                
                // increase difficulty 'spawn rate' of enemies in game manager 
                gameManager.SetDifficulty(1);
                
                // destroy enemy
                Destroy(gameObject);
            }
        }
        
        if (currentHealth > 0)
        {
            // update enemy position in relation to player
            transform.position = newPosition;

            // update enemy to face direction of player
            if (moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up * 0.7f);
            }
        }
    }


    /// <summary>
    /// This function returns current health of enemy.
    /// </summary>
    /// <returns>enemy's current health</returns>
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// This function sets enemies health.
    /// </summary>
    /// <param name="amount">value to modify enemy health</param>
    public void SetCurrentHealth(float amount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= amount;
        }
    }

    /// <summary>
    /// This function returns enemy's max health.
    /// </summary>
    /// <returns>enemy's max health</returns>
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// This function returns enemy's health bar.
    /// </summary>
    /// <returns>enemy's health bar</returns>
    public Healthbar GetHealth()
    {
        return health;
    }

    /// <summary>
    /// This function initiates the delay in enemy bounce back transition.
    /// </summary>
    public void Reset()
    {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// Function delays enemy bounce back recovery time.
    /// </summary>
    /// <returns>number of seconds to wait to execute code</returns>
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        state = MovingPattern.EaseIn;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && state != MovingPattern.Die)
        {
            print("Player collision");
            
            // reduce and update player health
            collision.gameObject.GetComponent<ThirdPersonController>().SetCurrentHealth(5);
            float current = collision.gameObject.GetComponent<ThirdPersonController>().GetCurrentHealth();
            float max = collision.gameObject.GetComponent<ThirdPersonController>().GetMaxHealth();
            collision.gameObject.GetComponent<ThirdPersonController>().GetHealth().UpdateHealth(current, max);

            state = MovingPattern.BackAway;
            Reset();
        }
    }
}
