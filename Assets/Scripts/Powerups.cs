using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public bool doublePoints;                       //For Doubling points
    public bool safeMode;                           //For removing spkies

    public float powerupLength;                     //Time for which the powerups last for

    private PowerupManager thePowerupManager;

    public Sprite[] powerupSprites;                 //holds color of the pickup

    // Start is called before the first frame update
    void Start()
    {
        thePowerupManager = FindObjectOfType<PowerupManager>();
    }

    void Awake()
    {
        int powerupSelector = Random.Range(0, 2);
        switch (powerupSelector)
        {
            case 0:doublePoints = true; break;
            case 1:safeMode     = true; break;
        }

        //Chainging color of powerups
        GetComponent<SpriteRenderer>().sprite = powerupSprites[powerupSelector];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            thePowerupManager.ActivatePowerup(doublePoints, safeMode, powerupLength);
        }
        gameObject.SetActive(false);
    }
}
