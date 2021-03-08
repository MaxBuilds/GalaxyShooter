using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUpBeahaviour : MonoBehaviour
{
    // Power up sound effects 
    [SerializeField]
    private AudioClip sound;

    // Update is called once per frame
   
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * 2.5f * Time.deltaTime);
        if (transform.position.y <= -7)
        {
            Destroy(this.gameObject);

        }
    }

    // Upon collision with player speed power up is activated.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.isFast())
            {
                AudioSource.PlayClipAtPoint(sound, transform.position);
                player.StopSpeed();
                player.SpeedActivated();
                Destroy(this.gameObject);
                
            }
            else
            {
                AudioSource.PlayClipAtPoint(sound, transform.position);
                player.SpeedActivated();
                Destroy(this.gameObject);
               
            }
            
        }

    }
}
