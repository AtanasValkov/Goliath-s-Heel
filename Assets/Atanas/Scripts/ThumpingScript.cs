using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumpingScript : MonoBehaviour
{
    public AudioSource aS;
    public AudioClip aC;
    private bool canPlay = false;

    private void Update()
    {
        if (canPlay == true && gameObject.transform.position.y <= 0.5)
        {
            aS.PlayOneShot(aC);
            canPlay = false;
        }

        if(gameObject.transform.position.y >= 1)
        {
            canPlay = true;
        }
    }

}
