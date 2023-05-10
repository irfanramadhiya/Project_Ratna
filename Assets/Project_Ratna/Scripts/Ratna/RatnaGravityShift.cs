using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatnaGravityShift : MonoBehaviour
{
    private Rigidbody2D rb; //ratna's Rigibody2D
    public RatnaController ratna; //references from RatnaController script
    public Animator anim; //references from Animator

    private bool onTop; //bool to check if Ratna is on the stage ceiling or not

    public float gravAnimDura; //duration of ratna's gravity shift animation
    public int gravShiftCharge; //Gravity Shift's charges to prevent infinite shifts
    public int gravShiftChargeValues; //the value of the charges
    
    void Start()
    {
        gravShiftCharge = gravShiftChargeValues; //set gravShiftCharge initial value
        ratna = GetComponent<RatnaController>();
        rb = GetComponent<Rigidbody2D>();        
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W) && gravShiftCharge > 0) //when W is pressed and only when the charges is above 0
        {
            gravShiftCharge--;
            rb.velocity = new Vector2(0, 0); //stops ratna's acceleration
            rb.gravityScale *= -1; //with each W press : gravity = 1, gravity = -1, gravity = 1 (-1*-1=1) 
            
            StartCoroutine("GravShiftLock"); //for animation & sprite rotation purpose    
        } 
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            gravShiftCharge = gravShiftChargeValues; 
            //had a problem with the value not decreasing when using ratna's isGround bool. Collider provided much more precise result.
        }
    }

    void Rotation() //basically rotates ratna's sprite 180 degree and horizontally
    {        
        if (onTop == false)
        {           
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {           
            transform.eulerAngles = Vector3.zero;
        }
        ratna.facingRight = !ratna.facingRight;
        onTop = !onTop;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public IEnumerator GravShiftLock()
    {       
        anim.SetBool("isGravShift", true);
        yield return new WaitForSeconds(gravAnimDura);
        Rotation();
        anim.SetBool("isGravShift", false);       
    }
}