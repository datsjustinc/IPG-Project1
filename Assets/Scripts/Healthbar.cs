using System;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

/// <summary>
/// This class manages health bar status.
/// </summary>
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float reduceSpeed = 2f;
    
    private float target = 1f;
    private Camera main;

    void Start()
    {
        // grab reference to main camera at the start
        main = Camera.main;
    }

    void Update()
    {
        // make sure health bar is always facing camera
        transform.rotation = Quaternion.LookRotation(transform.position - main.transform.position);

        // smoothly reduce the health bar amount over time
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
    
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        // stores percentage of health bar to show based on fill amount range (0-1)
        target = currentHealth / maxHealth;
    }
}
