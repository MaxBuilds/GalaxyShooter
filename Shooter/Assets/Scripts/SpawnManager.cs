using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject trippleShot;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private GameObject speed;
    [SerializeField]
    private GameObject asteroid;
    [SerializeField]
    private GameObject powerUpContainer;
    private bool playerLives = true;
    private AsteroidBehavior ast;
    private GameObject obj;
    private bool started = false;

    // Start is called before the first frame update
    // Asteroid spawned initially
    void Start()
    {
        SpawnAsteroid();
    }

    // Update is called once per frame
    // Upon asteroid destruction the spawning proccess begins.
    void Update()
    {
        if (ast.asteroid() && started == false)
        {
            started = true;
            StartCoroutine(SpawnOpps());
            StartCoroutine(SpawnTrippleShot());
            StartCoroutine(SpawnShieldUp());
            StartCoroutine(SpawnSpeedUp());
        }
        // If player dies all coroutines are stopped
        if(this.playerLives == false)
        {
            StopAllCoroutines();
        }
    }
    // Asteroid spawning function
    void SpawnAsteroid()
    {
        Vector3 position = new Vector3(0, 7, 0);
        obj = Instantiate(asteroid, position, Quaternion.identity);
        ast = obj.GetComponent<AsteroidBehavior>();
    }

    // Spawns enemies coroutine
    IEnumerator SpawnOpps()
    {
        new WaitForSeconds(10f);
        while (this.playerLives)
        {
            Vector3 position = new Vector3(Random.Range(-6f, 6f), 7, 0);
            GameObject obj = Instantiate(enemy, position, Quaternion.identity);
            obj.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(1f);
        }
    }

    // Spawn tripple shot power up coroutine
    IEnumerator SpawnTrippleShot()
    {
        new WaitForSeconds(10f);
        while (this.playerLives)
        {
            yield return new WaitForSeconds(Random.Range(0f, 80f));
            Vector3 position = new Vector3(Random.Range(-6f, 6f), 7, 0);
            GameObject obj = Instantiate(trippleShot, position, Quaternion.identity);
            obj.transform.parent = powerUpContainer.transform;
        }
    }

    // Spawn shield power up coroutine
    IEnumerator SpawnShieldUp()
    {
        new WaitForSeconds(10f);
        while (this.playerLives)
        {
            yield return new WaitForSeconds(Random.Range(0f, 80f));
            Vector3 position = new Vector3(Random.Range(-6f, 6f), 7, 0);
            GameObject obj = Instantiate(shield, position, Quaternion.identity);
            obj.transform.parent = powerUpContainer.transform;
            
        }
    }

    // Spawn speed power up coroutine
    IEnumerator SpawnSpeedUp()
    {
        new WaitForSeconds(10f);
        while (this.playerLives)
        {
            yield return new WaitForSeconds(Random.Range(0f, 80f));
            Vector3 position = new Vector3(Random.Range(-6f, 6f), 7, 0);
            GameObject obj = Instantiate(speed, position, Quaternion.identity);
            obj.transform.parent = powerUpContainer.transform;
        }
    }

    // Sets variable to false on player death
    public void OnDeath()
    {
        this.playerLives = false;
    }

}
