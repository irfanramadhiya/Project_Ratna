using UnityEngine;

public class Keys : MonoBehaviour
{
    //public GameObject gate;

    public bool locked = true;
    public Animator anim;
    public Rigidbody2D box;
    public AudioSource keyHoleActivated_Fx;
    public GameObject dashPadOff;
    public GameObject dashPadOn;

    void Update()
    {
        if (locked)
        {
            dashPadOff.SetActive(true);
            dashPadOn.SetActive(false);
        }
        else
        {
            dashPadOff.SetActive(false);
            dashPadOn.SetActive(true);
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Box")
        {
            keyHoleActivated_Fx.Play();
            Debug.Log("Key Accepted!!");
            anim.SetBool("isOn", true);
            locked = false;
            //gate.SetActive(false);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag != "Player")
        {
            locked = true;
            anim.SetBool("isOn", false);
        }
        
    }
}
