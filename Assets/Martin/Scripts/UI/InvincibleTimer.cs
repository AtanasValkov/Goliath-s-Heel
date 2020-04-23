using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InvincibleTimer : MonoBehaviour
{
    public Image filledTimer;

    public UnityEvent InvincibleEnd;

    public float timeInvincible;

    void SetChildren(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }

    void Awake()
    {
        SetChildren(false);
    }

    public void InvincibleStart()
    {
        StartCoroutine(Invincible());
    }

    IEnumerator Invincible()
    {
        float split = 0.1f;
        float rateOfFilling = split / timeInvincible;

        SetChildren(true);

        float filled = 1;
        
        while(filled > 0)
        {
            filledTimer.fillAmount = filled;

            yield return new WaitForSeconds(split);

            filled -= rateOfFilling;

            if (filled < 0) filled = 0; // just a precaution
        }

        filledTimer.fillAmount = filled;
        
        SetChildren(false);

        InvincibleEnd.Invoke();
    }
}
