/*
 * This script defines behaviour for the sshield power up upon activation.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{

    private int count = 2;
    private SpriteRenderer x;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        x = this.gameObject.transform.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player2D").transform.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player is dead the shield is automatically destroyed else its position is set to the center of the player.
        if (player.PisDead())
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = player.GetPosition();
        }
        
    }

    // If the player collides with an enemy the shield takes damage.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = GameObject.Find("Player2D").transform.GetComponent<Player>();
        if (player)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
                DamageShield();

            }
            // If shield collides with power up the shield power down is started over
            if (collision.gameObject.CompareTag("Shield"))
            {
                if (count == 2)
                {
                    Destroy(collision.gameObject);
                    Destroy(this.gameObject);
                    player.StopShield();
                    player.ShieldActivated();

                }
                else if (count == 1)
                {
                    count++;
                    Destroy(this.gameObject);
                    player.StopShield();
                    player.ShieldActivated();
                }

            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Shield damage function
    private void DamageShield()
    {
        count--;
        if(count == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            x.color = Color.red;
        }
    }
}
