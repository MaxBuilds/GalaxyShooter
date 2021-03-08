/*
 * This script defines the player behaviour and functionality.
 * It creates player movement and connects the player to the player system to monitour health and ammunition.
 * It also handles player health details and de=amange specifications.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Defining necessary variables.
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject trippleShot;
    [SerializeField]
    private GameObject shieldPrefab;
    private GameObject shprefab;
    private float canFire = -1f;
    private float fireRate = 0.15f;
    private bool overheat = false;
    private float maxHealth = 100.0f;
    private float currentHealth;
    private float ammo = 50.0f;
    private float ammoLeft;
    private bool isShieldActive = false;
    private bool isTrippleShotActive = false;
    private bool f_called = false;
    private float speed = 7.0f;
    private bool isSpeedActive = false;
    private bool isDead;
    private Animator m;

    // Start is called before the first frame update
    // Player stats are started at max and the position is set.
    void Start()
    {
        transform.position = new Vector3(0,-4,0);
        currentHealth = maxHealth;
        ammoLeft = ammo;
        isDead = false;
    }

    // Update is called once per frame
    // Movement and laser details are calculated an rendered. Shield position is also updated if present.
    void Update()
    {
        calculatemovvement();
        laserupdates();
        if(isShieldActive == true)
        {
            shieldPrefab.gameObject.transform.position = transform.position;
        }
    }

    // Function handles player movement and triggers animation for specific movement.
    private void calculatemovvement()
    {
        // Gets axis player input data
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // If player horizontal data is less than 0 i.e left trigger animation
        if(horizontalInput < 0)
        {
            m = this.GetComponent<Animator>();
            m.SetBool("MovingLeft",true);
            
        }
        // If player horizontal data is  0 not moving or same position remove all animation
        if (horizontalInput == 0)
        {
            m = this.GetComponent<Animator>();
            m.SetBool("MovingLeft", false);
            m.SetBool("MovingRight", false);

        }
        // If player horizontal data is greater than 0 i.e right trigger animation
        if (horizontalInput > 0)
        {
            m = this.GetComponent<Animator>();
            m.SetBool("MovingRight", true);
        }
        // Translate player according to player input data.
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        // Sets limits for player movement
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.9f)
        {
            transform.position = new Vector3(transform.position.x, -3.9f, 0);
        }

        if (transform.position.x >= 6.0f)
        {
            transform.position = new Vector3(6.0f, transform.position.y, 0);
        }
        else if (transform.position.x <= -6.0f)
        {
            transform.position = new Vector3(-6.0f, transform.position.y, 0);
        }
    }

    // Code updates ammunition bar and visual representation of lasers using the player system.
    private void laserupdates()
    {
        Vector3 offset = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
        GameObject system = GameObject.Find("PlayerSystem");
        Transform ammoBar = system.transform.GetChild(0);
        ammoBar = ammoBar.GetChild(1);
        ammoBar = ammoBar.GetChild(0);
        LineRenderer line = ammoBar.GetComponent<LineRenderer>();
        //if ammo == 0 then player cannot fire else the player can fire
        if (ammoLeft == 0)
        {
            if (isTrippleShotActive)
            {
                isTrippleShotActive = false;
            }
            line.SetPosition(0, new Vector3(-2.0f, 0, 0));
            overheat = true;
            if(f_called == false)
            {
                f_called = true;
                StartCoroutine(Overheat(line));
            }
            
        }
        else
        {
            if ((Input.GetKey(KeyCode.Space)) && Time.time > canFire && overheat == false)
            {
                ammoLeft--;
                float lineValue = (float)(ammoLeft / ammo) * 4;
                lineValue = -2 + lineValue;
                canFire = Time.time + fireRate;
                FireLaser(offset);               
                line.SetPosition(0, new Vector3(lineValue, 0, 0));
            }
            else
            {
                float lineValue = (float)(ammoLeft / ammo) * 4;
                lineValue = -2 + lineValue;
                line.SetPosition(0, new Vector3(lineValue, 0, 0));
            }
        }
        
     
    }

    //Damage system function for recieving enemy hits. Uses player system to updat health bar.
    public int Damage()
    {
        float damageDealt = Random.Range(10.0f, 20.0f);
        currentHealth -= damageDealt;
        GameObject system = GameObject.Find("PlayerSystem");
        Transform healthBar = system.transform.GetChild(1);
        healthBar = healthBar.GetChild(1);
        healthBar = healthBar.GetChild(0);
        float lineValue = (float) (currentHealth / maxHealth) * 4;
        lineValue -= 2;
        LineRenderer line = healthBar.GetComponent<LineRenderer>();
        if (lineValue <= -2)
        {
            isDead = true;
            line.SetPosition(0, new Vector3(-2.0f, 0, 0));
            GameObject yo = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(yo.gameObject, 2.37f);
            return 0;
        }
        else
        {
            line.SetPosition(0, new Vector3(lineValue, 0, 0));
            return 1;
        }
    }

    // Function instantiates lasers and fires them from player.
    private void FireLaser(Vector3 offset)
    {
        if(this.isTrippleShotActive)
        {
            Instantiate(trippleShot, offset, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, offset, Quaternion.identity);
        }
        
    }

    // Returns current speed of player
    public float GetSpeed()
    {
        return speed;
    }

    // Returns current player position
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // Activates tripple shot power up and begins power down routine.
    public void TrippleShotActivated()
    {
        this.isTrippleShotActive = true;
        this.ammoLeft = ammo;
        GameObject system = GameObject.Find("PlayerSystem");
        Transform ammoBar = system.transform.GetChild(0);
        ammoBar = ammoBar.GetChild(1);
        ammoBar = ammoBar.GetChild(0);
        LineRenderer line = ammoBar.GetComponent<LineRenderer>();
        line.startColor = Color.yellow;
        line.endColor = Color.yellow;
        line.SetPosition(0, new Vector3(2.0f, 0, 0));
        StartCoroutine(TrippleshotPowerDown(line));
    }

    // Tripple shot power down routine.
    private IEnumerator TrippleshotPowerDown(LineRenderer line)
    {
        yield return new WaitForSeconds(10.0f);
        isTrippleShotActive = false;
        line.startColor = Color.cyan;
        line.endColor = Color.cyan;
    }

    // Overheat function to realod player ammunition upon timeout.
    private IEnumerator Overheat(LineRenderer line)
    {
        yield return new WaitForSeconds(4.0f);
        line.startColor = Color.cyan;
        line.endColor = Color.cyan;
        ammoLeft = ammo;
        overheat = false;
        f_called = false;
    }

    // Speed power up activated and power down routine started
    public void SpeedActivated()
    {
        isSpeedActive = true;
        speed = 15.0f;
        StartCoroutine(SpeedPowerDown());
    }

    // Speed power down routine
    private IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        speed = 7.0f;
        isSpeedActive = false;
    }

    // Shield power up activated and power down routine started
    public void ShieldActivated()
    {
        isShieldActive = true;
        shprefab = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        StartCoroutine(ShieldPowerDown(shprefab));

    }

    // Shield power down routine
    private IEnumerator ShieldPowerDown(GameObject obj)
    {
        yield return new WaitForSeconds(5.0f);
        isShieldActive = false;
        Destroy(obj);
    }

    // Shield automatic stop
    public void StopShield()
    {
        isShieldActive = false;
        StopCoroutine(ShieldPowerDown(shprefab));
    }

    // Speed automatic stop
    public void StopSpeed()
    {
        StopCoroutine(SpeedPowerDown());
        speed = 7.0f;
    }

    // Checks if speed boost is active
    public bool isFast()
    {
        return isSpeedActive;
    }

    // Checks if player is dead
    public bool PisDead()
    {
        return isDead;
    }
}
