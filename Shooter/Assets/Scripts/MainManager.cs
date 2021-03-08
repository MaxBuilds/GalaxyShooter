/*
 * This script defines the main menu load function.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    // Loads game scene. 
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

}
