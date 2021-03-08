using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private float score;
    private Text score_txt;
    private GameObject gameOver;
    private GameObject restart;
    private bool playerDead = false;
    [SerializeField]
    private Sprite[] _worldlives;
    private int worldlife;
    private SpawnManager spawnm;
    // Start is called before the first frame update
    // Instantiates game objects
    void Start()
    {
        spawnm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        score_txt = this.gameObject.transform.GetChild(0).GetComponent<Text>();
        gameOver = this.gameObject.transform.GetChild(1).gameObject;
        restart = this.gameObject.transform.GetChild(2).gameObject;
        gameOver.SetActive(false);
        restart.SetActive(false);
        score = 0.0f;
        score_txt.text = "Score: " + score.ToString();
        worldlife = 3;
    }

    // Update is called once per frame
    // Appends screen string for score
    void Update()
    {
        score_txt.text = "Score: " + score.ToString();
        if (playerDead)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

    }

    // If enemy is killed increase score by 10
    public void enemyKilled()
    {
        score += 10.0f;
    }

    // If player is killed set the variable to true and start the game over blink
    public void playerKilled()
    {
        playerDead = true;
        StartCoroutine(GOBlink());
        
    }

    // Function to start game over blink
    private IEnumerator GOBlink()
    {
        while (true)
        {
            restart.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            if(gameOver.activeInHierarchy)
            {
                gameOver.SetActive(false);
            }
            else
            {
                gameOver.SetActive(true);
            }
            
        }
        
    }

    // If an enemy breaks through a world life is subtracted
    public void enemyBrokeThrough()
    {
        worldlife--;
        this.gameObject.transform.GetChild(3).GetComponent<Image>().sprite = _worldlives[worldlife];
        if(worldlife == 0)
        {
            GameObject player = GameObject.Find("Player2D");
            Destroy(player);
            playerKilled();
            spawnm.OnDeath();

        }
    }

    // Return the number of world lives
    public int getWorldLife()
    {
        return worldlife;
    }

}
