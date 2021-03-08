/*
 *This script defines the behavior of the lasers. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Laser moves up. If it leaves the screen without colliding it is automatically destroyed.
    void Update()
    {
        transform.Translate(new Vector3(0,4.0f,0) * Time.deltaTime * 5);


        if(transform.position.y >= 20)
        {
            Destroy(this.gameObject);
            if (this.gameObject.transform.parent)
            {
                Destroy(this.gameObject.transform.parent.gameObject);
            }
        }
    }
}
