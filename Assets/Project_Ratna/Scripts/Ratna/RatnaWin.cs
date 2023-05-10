using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatnaWin : MonoBehaviour
{

    public RatnaController rct;
    public RatnaGravityShift rgf;
    public PauseMenu pm;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FinishLine"))
        {
            pm.winMenuUI.SetActive(true);
            Time.timeScale = 0;
            rct.enabled = false;
            rgf.enabled = false;
        }
    }
}
