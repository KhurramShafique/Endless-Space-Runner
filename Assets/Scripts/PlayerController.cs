using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; //Player moveSpeed
    private float moveSpeedStore;//Store for our speed
    public float speedMultiplier;    //Multiplying moveSpeed
    public float jumpForce; //Force with which player will jump

    public float speedIncreaseMilestone;    //Amount of distance the player covers
    private float speedIncreaseMilestoneStore;
    private float speedMilestoneCount;  //Milestone reach hojae ga, to eg; 100 distance kiya, to 500 add hoga 
    private float speedMilestoneCountStore;
    
    public float jumpTime;  //button zada dair press hua, is se pata karein gey
    private float jumpTimeCounter; //kitni dair jumpTime press raha, ye dekhe ga
    private bool stoppedJumping;   //Eliminating Jump bug
    private bool canDoubleJump;     //For double jumping

    private Rigidbody2D myRB;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;   //Circle for GroundCheck
    public float groundCheckRadius;

    //Audio
    public AudioSource jumpSound;
    public AudioSource deathSound;

    //private Collider2D myCollider;

    private Animator myAnim;

    public GameManager theGameManager;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        //myCollider = GetComponent<Collider2D>();
        myAnim = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        stoppedJumping = true;
        canDoubleJump  = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Checking whether player is on the ground or not
        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //What moveSpeed is
        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            //milstone jo dal rahay hain, agla us se zada ho, takay jaldi milestone reach na ho
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;

            //speed up Player
            moveSpeed = moveSpeed * speedMultiplier;
        }

        myRB.velocity = new Vector2(moveSpeed, myRB.velocity.y);

        //Checking whether player jumping or not
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            //For single Jump
            if (grounded)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play();
            }

            //For double Jump
            if (!grounded && canDoubleJump)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

        //Jump kitni dair krna hai, vo calculate ho rha
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if (jumpTimeCounter > 0)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        //Agar jump button chor diya hai
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        //reset jumpCountTimer
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }

        myAnim.SetFloat("Speed", myRB.velocity.x); //bta rahay speed animator ko
        myAnim.SetBool("Grounded", grounded); //bta rahay animator ko k ground hai ya nahi
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "KillBox")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            deathSound.Play();
        }
    }
}
