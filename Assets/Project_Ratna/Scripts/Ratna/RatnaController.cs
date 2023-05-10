using System.Collections;
using UnityEngine;

public class RatnaController : MonoBehaviour {
    public float speed; //Ratna's movement speed
    public float jumpForce; //Ratna's jump force and max height
    public float moveInput; //check for user input

    public float dashSpeed; //Ratna's dash speed
    public int dashChargesValues; //dash charge value
    public float dashDuration; //duration of dash
    public float dashWait; //duration to wait after dash
    public int dashCharges; //amount of dash
    public float isLocking; //locking ratna when dashing
    public bool isDashing; //bool to check if ratna is dashing

    public float slamSpeed; //ratna's slam speed
    public float slamWait; //lock duration after slam
    public bool isSlamming; //to check if ratna is slamming the ground
    public bool smash;

    public Animator anim; //Ratna's animator

    private Rigidbody2D rb; //make Ratna moves with Rigidbody2D

    public bool facingRight; //check what direction ratna is facing

    public bool isGrounded; //check if Ratna is grounded or not
    public Transform groundCheck; //a circle below Ratna's feet, to detect ground
    public float checkRadius; //the size of the circle
    public LayerMask whatIsGrounded; //determine what is the ground

    public int doubleJump; //for double jump
    public int doubleJumpValue; //double jump charges value

    public bool isPushing; //check if ratna is pushing a block or not

    public RatnaController rct;
    public RatnaGravityShift rgf;

    void Start()
    {
        smash = false;
        rct.enabled = true;
        rgf.enabled = true;
        facingRight = true;
        anim.SetBool("isAlive", true);
        isLocking = dashDuration;
        dashCharges = dashChargesValues;
        doubleJump = doubleJumpValue;
        rb = GetComponent<Rigidbody2D>(); //get value for rigidbody2D
    }

    void FixedUpdate()
    {
        //Ground Check Initialize
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGrounded);

        //Horizontal Movement Mechanism
        moveInput = Input.GetAxisRaw("Horizontal"); //value either 1 (Right) or -1 (Left) when pressing D/-> (1) or A/<- (-1)
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); //Moves Ratna to either left or right
        anim.SetFloat("ratnaSpeed", Mathf.Abs(moveInput));

        //Grounded & Mid-Air Animation Controller
        if(rb.gravityScale > 0) //in normal gravity condition
        {
            if(isGrounded == true)
            {
                anim.SetBool("isGrounded", true);
                anim.SetBool("isFalling", false);
                doubleJump = doubleJumpValue;

                anim.SetBool("isJumping", false);
                anim.SetBool("isDoubleJump", false);

                anim.SetBool("isSmash", false);
                anim.SetBool("isSmashLand", true);
            }else if(isGrounded != true)
            {
                anim.SetBool("isGrounded", false);
            
                anim.SetBool("isJumping", true);  
            }
            
            if(rb.velocity.y > 0 && doubleJump == 0)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", false);
                anim.SetBool("isDoubleJump", true);
            }else if(rb.velocity.y < -0.5)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isDoubleJump", false);
                anim.SetBool("isFalling", true);    
            }

        }else if(rb.gravityScale < 0) //in shifted gravity condition
        {
            if (isGrounded == true)
            {
                anim.SetBool("isGrounded", true);
                anim.SetBool("isFalling", false);
                doubleJump = doubleJumpValue;

                anim.SetBool("isJumping", false);
                anim.SetBool("isDoubleJump", false);

                anim.SetBool("isSmash", false);
                anim.SetBool("isSmashLand", true);
            }
            else if (isGrounded != true)
            {
                anim.SetBool("isGrounded", false);

                anim.SetBool("isJumping", true);
            }

            if (rb.velocity.y < 0 && doubleJump == 0)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", false);
                anim.SetBool("isDoubleJump", true);
            }
            else if (rb.velocity.y > -0.5 && isGrounded != true)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", true);
                anim.SetBool("isDoubleJump", false);
            }


        }
        
        //Slam Move
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("isSmash", true);
            if (isGrounded != true)
            {
                StartCoroutine("SlamPause");
                if (rb.gravityScale > 0)
                {   
                    rb.velocity = new Vector2(0, -slamSpeed);
                }
                else if (rb.gravityScale < 0)
                {    
                    rb.velocity = new Vector2(0, slamSpeed);
                }
            }
        }

        //Flipping Ratna's Sprite
        if (facingRight == false && moveInput > 0) // (O-O  ) --->
        {
            Flip();
        } else if (facingRight == true && moveInput < 0) // <--- (  O-O)
        {
            Flip();
        }
    }

    void Update()
    {
        //Jump & Double Jump Mechanics
        if (Input.GetKeyDown(KeyCode.Space) && doubleJump > 0)
        {
            doubleJump--;
            if (rb.gravityScale > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            else if (rb.gravityScale < 0)
            {
                rb.velocity = (Vector2.up * jumpForce) * -1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump == 0 && isGrounded == true) //prevent infinite jumps
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
            anim.SetBool("isGrounded", false);
            anim.SetBool("isDoubleJump", true);
            isGrounded = !isGrounded;
            if (rb.gravityScale > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            else if (rb.gravityScale < 0)
            {
                rb.velocity = (Vector2.up * jumpForce) * -1;
            }
        }
        
        //Dash Mechanics
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCharges > 0 && isPushing == false)
        {
            StartCoroutine("DashMove");
            if (isDashing == true)
            {
                isLocking -= Time.deltaTime;
                if (isLocking == 0)
                {
                    isDashing = false;
                }
            }
            isLocking = dashDuration;          
        }
    }

    //Block Pushing Mechanics with Collider as a trigger
    //1. On colliding with the trigger
    public void OnTriggerEnter2D(Collider2D coll) //in testing, OnTriggerEnter2D provided much better result than OnCollisionEnter2D
    {
        if (coll.CompareTag("Box"))
        {
            isPushing = true;
            anim.SetBool("isPushing", true);
        }
    }
    //2. On exiting the trigger
    void OnTriggerExit2D(Collider2D coll)
    {
        isPushing = false;
        anim.SetBool("isPushing", false);
    }

    void Flip() //Flip Ratna's sprite when moving to the opposite direction
    {
        //if sprite facing right, but moving to left, change facingRight to false, then flip the sprite by changine the x scale to negative
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    public IEnumerator SlamPause() //Locks Ratna's movement when slam landing, for I M M E R S I O N  :)
    {
        isSlamming = true;
        smash = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(slamWait);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        smash = false;
        isSlamming = false;
    }
    public IEnumerator DashMove() //Speeds up Ratna's movement speed for a period of time
    {
        anim.SetBool("isDashing", true);
        isDashing = true;
        dashCharges--;
        speed += dashSpeed;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(dashDuration);
        anim.SetBool("isDashing", false);
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        speed -= dashSpeed;
        yield return new WaitForSeconds(dashWait);
        dashCharges++;
    }
}