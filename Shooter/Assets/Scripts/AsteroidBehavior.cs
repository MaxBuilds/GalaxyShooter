/* 
This scripts defines the behaviour of the 
asteroid created in the beginning of the game.
It defines its motion and trigger in the event of a collision.
*/

// Neccesary files included
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{

    // Necessary variables are defined such as the explosion game object and the spawn manager.
    [SerializeField]
    private GameObject explosion;
    private SpawnManager spawnManager;
    private float rotateSpeed = 25.0f;
    private Vector3 idpos;
    private float speed = 2.5f;
    private bool astroidGone = false;

    // Start is called before the first frame update.
    // Spawn manger and identity position of the asteroid are initiallized. 
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        idpos = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    // Every update the asteroid position is translated moving down on screen untill it reaches the identity position.
    // Once it reaches this position it begins to rotate and maintains that position.
    void Update()
    {
        
        if(transform.position.y > 1)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            transform.position = idpos;
        }
        
    }

    // On collision with the players lasers the explosion sequence is startedand the game begins.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            Destroy(collision.gameObject);
            GameObject yo = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.25f);        
            Destroy(yo.gameObject, 2.37f);
            astroidGone = true;
            
        }
    }

    // Returns whether or not the asteroid is gone.
    public bool asteroid()
    {
        return astroidGone;
    }
}
