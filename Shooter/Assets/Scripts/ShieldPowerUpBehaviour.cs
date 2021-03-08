using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUpBehaviour : MonoBehaviour
{
    // Power up sound effects 
    [SerializeField]
    private AudioClip sound;

    // Update is called once per frame
    // Object moves down till it is out of screen then it is destroyed.
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * 2.5f * Time.deltaTime);
        if (transform.position.y <= -7)
        {
            Destroy(this.gameObject);

        }

    }

    // Upon collision with player shield is activated.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(sound, transform.position);
            Player player = collision.gameObject.GetComponent<Player>();
            player.ShieldActivated();
            Destroy(this.gameObject);
            
        }

    }

}
