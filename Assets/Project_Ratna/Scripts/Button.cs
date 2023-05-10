using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public GameObject gate;
    public bool locked = true;
    public RatnaController movement;
    public Animator anim;
    public AudioSource buttonPressed_Fx;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && movement.smash == true)
        {
            buttonPressed_Fx.Play();
            anim.SetBool("isOn", true);
            locked = false;
            gate.SetActive(false);
        }
    }
}
