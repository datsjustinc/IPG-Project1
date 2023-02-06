using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the spawning of enemy AI's.
/// </summary>
public class GameManager : MonoBehaviour
{
    // keeps track of number of enemies in a list
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private int difficulty = 1;
    
    /// Start of spawning a single enemy
    private void Start()
    {
        StartCoroutine(Spawn(1f));
    }
    
    /// <summary>
    /// This function updates every frame and keeps track of
    /// how many enemies should be on screen at a time.
    /// </summary>
    private void Update()
    {
        if (enemies.Count < difficulty)
        {
            // spawn enemies with a delay
            StartCoroutine(Spawn(1f));
        }
    }

    /// <summary>
    /// This function spawns enemies after a certain wait time.
    /// </summary>
    /// <param name="delay">amount of time to wait</param>
    /// <returns>number of seconds to wait to execute code</returns>
    private IEnumerator Spawn(float delay)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemies.Add(enemy);
        yield return new WaitForSeconds(delay);
        Vector3 randomLocation = new Vector3(Random.Range(-50f, 50f), 0.7f, Random.Range(-50f, 50f));
        enemy.transform.position = randomLocation;
    }

    /// <summary>
    /// This function returns list that stores all enemy objects.
    /// </summary>
    /// <returns>list of enemy objects</returns>
    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    /// <summary>
    /// This function modifies difficulty of enemy spawn rate.
    /// </summary>
    /// <param name="amount">value to increase spawn rate by</param>
    public void SetDifficulty(int amount)
    {
        difficulty += amount;
    }
}
