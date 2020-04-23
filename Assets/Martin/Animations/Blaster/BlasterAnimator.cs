using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterAnimator : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShotFired(bool hitTarget)
    {
        animator.SetBool("hitTarget", hitTarget);
        animator.Play("BlasterFired");
    }
}
