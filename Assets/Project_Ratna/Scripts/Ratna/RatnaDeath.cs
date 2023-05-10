using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatnaDeath : MonoBehaviour
{
    public Rigidbody2D rb;
    public RatnaController rct;
    public RatnaGravityShift rgf;
    public PauseMenu ps;
    public Animator anim;

    public bool ratnaIsDead; //bool to check if ratna is dead or not :v
    void Update()
    {
        if (ratnaIsDead == true)
        {
            ps.deathMenuUI.SetActive(true); //set deathMenu UI active 
            anim.SetBool("isAlive", false);
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Traps") //when ratna's collider collides with anything that has "Traps" tag, she will die
        {
            rct.enabled = false; //prevents player movement input
            rgf.enabled = false; //as well as player's ability to grav shift
            rb.constraints = RigidbodyConstraints2D.FreezeAll; //and for safety measure, locks player Rigidbody2D in all axis :)
            anim.SetBool("isDead", true);
            ratnaIsDead = true;
        }
    }
}