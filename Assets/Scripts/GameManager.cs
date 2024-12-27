using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform platformGenerator;         // The platform generator object
    private Vector3 platformStartPoint;         // Starting point of the platform generator

    public PlayerController thePlayer;          // Reference to the player controller
    private Vector3 playerStartPoint;           // Starting point of the player

    private PlatformDestroyer[] platformList;   // Array to store platforms for disabling

    private ScoreManager theScoreManager;       //Reference to ScoreManager

    public DeathMenu theDeathScreen;            //Reference to MainMenu

    public bool powerupReset;                   //Will reset our powerups

    // Start is called before the first frame update
    void Start()
    {
        platformStartPoint = platformGenerator.position;   // Store platform generator's starting point
        playerStartPoint = thePlayer.transform.position;   // Store player's starting point
        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);  // Hide the player

        //Turn on Death Screen
        theDeathScreen.gameObject.SetActive(true);

        //StartCoroutine("RestartGameCo");
    }

    public void Reset()
    {
        //Turn on Death Screen
        theDeathScreen.gameObject.SetActive(false);

        // Find all platforms in the scene and disable them
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);  // Disable each platform
        }

        // Reset player and platform generator to their start points
        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;

        thePlayer.gameObject.SetActive(true);  // Make the player visible again

        theScoreManager.scoreCount = 0;         //Setting score to 0
        theScoreManager.scoreIncreasing = true; //Setting scoreIncreasing to true

        powerupReset = true;
    }

    /*public IEnumerator RestartGameCo()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);  // Hide the player
        yield return new WaitForSeconds(0.5f);  // Add a delay

        // Find all platforms in the scene and disable them
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);  // Disable each platform
        }

        // Reset player and platform generator to their start points
        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;

        thePlayer.gameObject.SetActive(true);  // Make the player visible again

        theScoreManager.scoreCount = 0;         //Setting score to 0
        theScoreManager.scoreIncreasing = true; //Setting scoreIncreasing to true
    }*/
}
