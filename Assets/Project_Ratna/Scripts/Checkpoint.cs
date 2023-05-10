using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameMaster gm;
    public Animator anim;
    public AudioSource Check_Fx;
    public bool checkIsActive;

    void Start()
    {
        checkIsActive = false;
        gm = GameObject.FindObjectOfType<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Check_Fx.Play();
            anim.SetBool("isActivated", true);
            gm.lastCheckPointPos = transform.position;
        }
    }
}