using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGrav : MonoBehaviour
{
    private Rigidbody2D rb;
    public int boxShiftCharge;
    public RatnaGravityShift rgf;

    void Start()
    {
        boxShiftCharge = 1;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W) && boxShiftCharge > 0)
        {
            boxShiftCharge--;
            rb.gravityScale *= -1;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            boxShiftCharge = 1;
        }

        if(collision.gameObject.tag == "Keys")
        {
            boxShiftCharge = 0;
        }
    }
}
