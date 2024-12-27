using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController thePlayer;

    private Vector3 lastPlayerPosition; //Position of player
    private float distanceToMove;       //Camera move karwana hai x ammount, relating to player

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        lastPlayerPosition = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera move karwa rahay player ki position k mutabiq
        distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
        transform.position = new Vector3(transform.position.x + distanceToMove,transform.position.y, transform.position.z);  
        lastPlayerPosition = thePlayer.transform.position;
    }
}
