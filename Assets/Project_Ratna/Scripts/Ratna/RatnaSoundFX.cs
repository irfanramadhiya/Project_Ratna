using UnityEngine.Audio;
using System.Collections;
using UnityEngine;

public class RatnaSoundFX : MonoBehaviour
{
    public AudioSource RatnaFx;
    public AudioClip JumpFx;
    public AudioClip LandingFx;
    public AudioSource Landing_Fx;
    public AudioClip Smash_LandingFx;
    public AudioClip DashFx;
    public AudioSource Push_Fx;
    public AudioClip Gravity_ShiftFx;
    public AudioClip DeadFx; 
    public AudioClip RespawnFx;

    public RatnaDeath rd;
    public RatnaController rct;
    public RatnaGravityShift rgf;
    public Rigidbody2D rb;

    public bool isLanding; //bool to check if ratna is landing after jumping
    
    void Start()
    {
        RatnaFx.PlayOneShot(RespawnFx); //respawn sound fx
    }
    void Update()  
    {
        if(rd.ratnaIsDead != true) //only plays sound fx if ratna is still alive
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RatnaFx.PlayOneShot(JumpFx); //jumping sound fx, still spammable though... 
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (rct.isSlamming == true)
                {
                    RatnaFx.PlayOneShot(Smash_LandingFx);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && rct.isPushing == false) 
            {
                RatnaFx.PlayOneShot(DashFx);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                RatnaFx.PlayOneShot(Gravity_ShiftFx);
            }
        }    
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Traps")
        {
            RatnaFx.PlayOneShot(DeadFx);
        }

        if(coll.gameObject.tag == "Ground")
        {
            isLanding = true;
            if(!Landing_Fx.isPlaying && isLanding == true)
            {
                isLanding = false;
                Landing_Fx.PlayOneShot(LandingFx);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D coll) //when pushing blocks
    {
        if (coll.CompareTag("Box") && rct.moveInput != 0)
        { 
            Push_Fx.Play();   
        }
    }

    private void OnTriggerExit2D(Collider2D coll) //when not pushing blocks
    {
        if (coll.CompareTag("Box"))
        {
            Push_Fx.Stop();
        }
    }   
}