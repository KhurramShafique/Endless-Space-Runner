using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;      // Platform that will be generated
    public Transform generationPoint;   // Same "PlatformGenerationPoint" yahan use hoga
    public float distanceBetween;       // Platform se distance kitna hai

    private float platformWidth;        // For different sized platforms

    // Different size k platform k liye
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    // Value jo decide karay gi, k kisko choose krna hai
    private int platformSelector;
    // public GameObject[] thePlatforms;
    private float[] platformWidths;

    // Reference to ObjectPooler
    public ObjectPooler[] theObjectPools;

    // Controlling height of platforms
    private float minHeight;
    public Transform maxHeightPoint;
    private float maxheight;
    public float maxHeightChange;       // Heights mai difference kitna hai
    private float heightChange;

    //Reference to CoinGenerator Script
    private CoinGenerator theCoinGenerator;
    public float randomCoinThreshold;

    //Spikes
    public float randomSpikeThreshold;
    public ObjectPooler spikePool;

    public float powerupHeight;         //Height for platformGenerator
    public ObjectPooler powerupPool;    //Reference to powerup object pool
    public float powerupThreshold;      //Numbwer to randomly appear

    // Start is called before the first frame update
    void Start()
    {
        // Get the width of each platform from the object pool
        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        // Initialize minHeight and maxheight for platform generation
        minHeight = transform.position.y;         // Minimum height for platforms
        maxheight = maxHeightPoint.position.y;    // Maximum height point reference

        theCoinGenerator = FindObjectOfType<CoinGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Generate platform when the generator passes the generation point
        if (transform.position.x < generationPoint.position.x)
        {
            // Choose a random distance between platforms
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            // Randomly select a platform from the object pools
            platformSelector = Random.Range(0, theObjectPools.Length);

            // Calculate height change with a random value within bounds
            heightChange = transform.position.y + Random.Range(-maxHeightChange, maxHeightChange);

            // Ensure the new platform's height doesn't go out of bounds
            if (heightChange > maxheight)
            {
                heightChange = maxheight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            //Randomly generating powerup
            if(Random.Range(0f, 100f) < powerupThreshold)
            {
                GameObject newPowerup = powerupPool.GetPooledObject();
                newPowerup.transform.position = transform.position + new Vector3(distanceBetween/2f, Random.Range(powerupHeight/2, powerupHeight), 0f);
                newPowerup.SetActive(true);
            }

            // Move the platform generator to the new position
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            // Get a platform from the object pool and activate it
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0f, 100f) < randomCoinThreshold)
            {
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }

            // Randomly decide where to generate spikes
            if (Random.Range(0f, 100f) < randomSpikeThreshold)
            {
                GameObject newSpike = spikePool.GetPooledObject(); // Fixed typo

                float spikeXPosition = Random.Range(-platformWidths[platformSelector]/2f + 1f, platformWidths[platformSelector] / 2f - 1f);

                Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f, 0f);

                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

            // Move the platform generator forward after placing the platform
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);
        }
    }
}