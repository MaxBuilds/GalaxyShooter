/*
 * This script defines the behaviour for the enemy ships.
 */

// Neccesary files included
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    // Necessary variables are defined such as the spawn\ manager.
    private SpawnManager spawnManager;
    private UIManager ui;
    Animator m;
    private bool untouched = true;
    private float speed = 2.5f;
    [SerializeField]
    private AudioClip sound;
    private AudioSource audioData;

    // Start is called before the first frame update
    // Spawn manager, ui manager, audio source and animator initialized.
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        m = this.gameObject.GetComponent<Animator>();
        this.audioData = this.gameObject.GetComponent<AudioSource>();
        this.audioData.clip = this.sound;
    }

    // Update is called once per frame
    // Enemy motion moves downward. If the enemy should leave the screen destroy it and call function to reduce world life.
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
        if(transform.position.y <= -7)
        {
            Destroy(this.gameObject);
            if(ui.getWorldLife() > 0)
            {
                ui.enemyBrokeThrough();
            }            

        }
    }

    // On collision with laser both objects are destroyed.
    // On collision with player the player takes damage and the enemy is destroyed.
    private void OnTriggerEnter2D(Collider2D other)
    {
        untouched = false;
        if (other.gameObject.CompareTag("Laser") && untouched == false)
        {
            PolygonCollider2D opp_p = this.gameObject.GetComponent<PolygonCollider2D>();
            Destroy(opp_p);
            this.speed = 0;
            m.SetTrigger("OnEnemyDeath");
            Destroy(other.gameObject);
            ui.enemyKilled();   
            Destroy(this.gameObject, 2.5f);
            audioData.Play();
        }
        if(other.gameObject.name == "Player2D" && untouched == false)
        {
            PolygonCollider2D opp_p = this.gameObject.GetComponent<PolygonCollider2D>();
            Destroy(opp_p);
            this.speed = 0;
            m.SetTrigger("OnEnemyDeath");
            Player player = other.gameObject.GetComponent<Player>();
            int i = player.Damage();
            if(i == 0)
            {
                spawnManager.OnDeath();
                ui.playerKilled();
                Destroy(other.gameObject);
            }
            Destroy(this.gameObject, 2.5f);
            audioData.Play();
        }
    }
}
