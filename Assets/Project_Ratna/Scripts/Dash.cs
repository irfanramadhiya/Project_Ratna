using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D RatnaBody;
    public RatnaController RatnaController;
    public float dashzoneSpeed;
    public float dashzoneDuration;

    public bool isUp, isDown, isLeft, isRight;


    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            DashOn();
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        Invoke("DashOff", dashzoneDuration);
    }

    public void DashOn()
    {
            
        if (isRight)
        {
            RatnaBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            RatnaBody.velocity = new Vector2(dashzoneSpeed, 0);
        }else if (isLeft)
        {
            RatnaBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            RatnaBody.velocity = new Vector2(-dashzoneSpeed, 0);
        }else if (isUp)
        {
            RatnaBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            RatnaBody.velocity = new Vector2(0, dashzoneSpeed);
        }else if (isDown)
        {
            RatnaBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            RatnaBody.velocity = new Vector2(0, -dashzoneSpeed);
        }
            
        RatnaController.enabled = false;
        anim.SetBool("isDashing", true);
    }
    public void DashOff()
    {
        RatnaBody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        RatnaController.enabled = true;
        anim.SetBool("isDashing", false);
    }
}
