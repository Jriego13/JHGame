using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform groundCheck, groundCheckL, groundCheckR;
    Transform playerPosition;
    Animator playerAnimator;
    Rigidbody2D playerBody;
    SpriteRenderer playerSprite;
    public bool grounded;
    public bool running;
    public bool ducking;
    public bool shooting;

    // Start is called before the first frame update
    void Start() // it seems like a lot of getcomponents happen here so that we can use them later.
    {
        playerAnimator = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerPosition = GetComponent<Transform>();
        //sprites = Resources.LoadAll("Assets/Warped Caves/Artwork/Sprites/player/player-duck");
    }

    void Update() // use update to get input
    {

    }

    // FixedUpdate is called after a particular number of frames, use fixedupdate to affect movement
    void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
          (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) ||
          (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetKey("a"))
        {
            playerPosition.eulerAngles = new Vector2(0, 180);
            running = true;
            if (!ducking)
                playerBody.velocity = new Vector2(-1, playerBody.velocity.y);
            if (grounded && !ducking && !shooting)
                playerAnimator.Play("PlayerRun");
        }
        else if (Input.GetKey("d"))
        {
            playerPosition.eulerAngles = new Vector2(0, 0);
            running = true;
            if (!ducking)
                playerBody.velocity = new Vector2(1, playerBody.velocity.y);
            if (grounded && !ducking && !shooting)
                playerAnimator.Play("PlayerRun");
        }
        else
        { 
            playerBody.velocity = new Vector2(0, playerBody.velocity.y);
            running = false;
            if (grounded && !ducking && !shooting)
                playerAnimator.Play("PlayerIdle");
        }

        if (Input.GetKey("s"))
        {
            ducking = true;
            playerAnimator.Play("PlayerDuck");
        }
        else
            ducking = false;

        if (Input.GetKey("e") && !running)
        {
            playerAnimator.Play("PlayerStandShoot");
            shooting = true;
        }
        else if (Input.GetKey("e") && running)
        {
            playerAnimator.Play("PlayerRunShoot");
            shooting = true;
        }
        else
            shooting = false;

        if (Input.GetKey("space") && grounded)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, 3);
        }

        if (!grounded)
        {
            playerAnimator.Play("PlayerJump");
        }
    }
}
