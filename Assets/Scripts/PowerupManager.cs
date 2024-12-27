using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private bool doublePoints;              // For Doubling points
    private bool safeMode;                  // For removing spikes

    private bool powerupActive;             // Is powerup active?
    private float powerupLengthCounter;     // Time remaining for powerup

    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;

    private float normalPointsPerSecond;    // Reference to pointsPerSeconds in ScoreManager
    private float spikeRate;                // Reference to randomSpikeThreshold in PlatformGenerator
    private PlatformDestroyer[] spikeList;  // To hold all spike platforms
    private GameManager theGameManager;     //Reference to GameManager

    // Start is called before the first frame update
    void Start()
    {
        // Null check to avoid runtime errors
        theScoreManager      = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theGameManager       = FindObjectOfType<GameManager>();

        if (theScoreManager == null)
        {
            Debug.LogError("ScoreManager not found!");
        }
        if (thePlatformGenerator == null)
        {
            Debug.LogError("PlatformGenerator not found!");
        }if (theGameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupActive)
        {
            powerupLengthCounter -= Time.deltaTime;

            if (theGameManager.powerupReset)
            {
                powerupLengthCounter = 0;
                theGameManager.powerupReset = false;
            }

            // Double points logic
            if (doublePoints)
            {
                theScoreManager.pointsPerSeconds = normalPointsPerSecond * 2f;
                theScoreManager.shouldDouble = true;
            }

            // Safe mode logic (disabling spikes)
            if (safeMode)
            {
                thePlatformGenerator.randomSpikeThreshold = 0f;  // No spikes during safe mode
            }

            // Powerup time has ended
            if (powerupLengthCounter <= 0)
            {
                // Reset to normal values
                theScoreManager.pointsPerSeconds = normalPointsPerSecond;
                theScoreManager.shouldDouble = false;
                thePlatformGenerator.randomSpikeThreshold = spikeRate;

                // Re-enable spikes after safe mode ends
                if (safeMode)
                {
                    for (int i = 0; i < spikeList.Length; i++)
                    {
                        if (spikeList[i].gameObject.name.Contains("Spikes"))
                        {
                            spikeList[i].gameObject.SetActive(true);  // Re-enable spikes
                        }
                    }
                }

                powerupActive = false;  // Powerup is no longer active
            }
        }
    }

    // Activating the powerup with parameters
    public void ActivatePowerup(bool points, bool safe, float time)
    {
        doublePoints = points;
        safeMode = safe;
        powerupLengthCounter = time;

        // Store original values for resetting
        normalPointsPerSecond = theScoreManager.pointsPerSeconds;
        spikeRate = thePlatformGenerator.randomSpikeThreshold;

        if (safeMode)
        {
            // Find all spike platforms and disable them for safe mode
            spikeList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("Spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);  // Disable spikes
                }
            }
        }

        powerupActive = true;  // Activate the powerup
    }
}